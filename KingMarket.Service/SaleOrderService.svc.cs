﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KingMarket.Data.Infrastructure;
using KingMarket.Data.Repositories;
using KingMarket.Model.Models;
using KingMarket.Utility;

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SaleOrderService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SaleOrderService.svc or SaleOrderService.svc.cs at the Solution Explorer and start debugging.
    public class SaleOrderService : ISaleOrderService
    {
        private readonly ISaleOrderRepository repository;
        private readonly IProductRepository productRepository;
        private readonly ICartItemRepository cartItemrepository;
        private readonly IUnitOfWork unitOfWork;

        public SaleOrderService()
        {
            var db = new DbFactory();
            repository = new SaleOrderRepository(db);
            unitOfWork = new UnitOfWork(db);
            productRepository = new ProductRepository(db);
            cartItemrepository = new CartItemRepository(db);
        }

        public IEnumerable<SaleOrder> GetSaleOrdersByCustomerId(int customerId)
        {
            return repository.GetMany(c => c.CustomerId.Equals(customerId));
        }

        public SaleOrder GetSaleOrder(int id)
        {
            var myObject = repository.GetById(id);
            return myObject;
        }

        public void CreateSaleOrder(SaleOrder myObject)
        {
            try
            {
                //ASIGNO FECHA ACTUAL A LA ORDEN
                myObject.DateOrder = DateTime.Now;
                //VERIFICO SI:
                var products = new List<Product>();
                foreach (var item in myObject.SaleOrderDetails)
                {
                    var product = productRepository.GetById(item.ProductId);
                    //ARTICULO EXISTE
                    if (product == null)
                        throw new FaultException<GeneralException>(new GeneralException()
                        {
                            Id = "SO-C-01",
                            Description = string.Format("Article: {0} no longer exists.", item.ProductId)
                        }, new FaultReason("Error when trying to create."));
                    //ARTICULO TIENE STOCK
                    if (product.Stock < item.Quantity)
                        throw new FaultException<GeneralException>(new GeneralException()
                        {
                            Id = "BO-C-02",
                            Description = string.Format("The article: {0} has sufficient stock.", product.Name)
                        }, new FaultReason("Error when trying to create."));
                    product.Stock -= item.Quantity;
                    products.Add(product);
                }
                //CREO LA ORDEN
                var saleOrder = repository.GetAll().OrderByDescending(o => o.SaleOrderId).FirstOrDefault();
                //var saleOrder = repository.GetAll().Max();
                myObject.DocumentNumber = saleOrder == null
                    ? ("1").PadLeft(10, '0')
                    : (saleOrder.SaleOrderId + 1).ToString().PadLeft(10, '0');
                repository.Add(myObject);
                //ACTUALIZO PRODUCTOS
                foreach (var item in products)
                    productRepository.Update(item);
                //VACIO CARRITO DE COMPRAS
                cartItemrepository.Delete(c => c.CustomerId.Equals(myObject.CustomerId));
                unitOfWork.Commit();
                //CREO LA COLA DE MENSAJES CON LOS ARTICULOS 
                //QUE TIENEN STOCK POR DEBAJO DE STOCK MINIMO
                //Y QUE NO ESTEN EN LA COLA
                var pathQueue = @".\private$\products";
                var productsQueue = Utilities.ReadQueue<ProductQueue>(pathQueue);
                foreach (var item in products)
                {
                    item.CartItems = null;
                    if (item.Stock > item.MinStock) continue;
                    if(productsQueue == null)
                        Utilities.WriteQueue(pathQueue, new ProductQueue {ProductId = item.ProductId, Name = item.Name}, String.Format("Product: {0}", item.ProductId));
                    else
                    {
                        var product = productsQueue.Find(p => p.ProductId.Equals(item.ProductId));
                        if(product == null)
                            Utilities.WriteQueue(pathQueue, new ProductQueue { ProductId = item.ProductId, Name = item.Name }, String.Format("Product: {0}", item.ProductId));
                    }
                }
            }
            catch (FaultException<GeneralException> gException)
            {
                throw gException;
            }
            catch (Exception ex)
            {
                throw Utilities.GetException(ex, "create.");
            }
        }
    }
}