using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KingMarket.Data.Infrastructure;
using KingMarket.Data.Repositories;
using KingMarket.Model.Models;

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SaleOrderService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SaleOrderService.svc or SaleOrderService.svc.cs at the Solution Explorer and start debugging.
    public class SaleOrderService : ISaleOrderService
    {
        private readonly ISaleOrderRepository repository;
        private readonly IProductRepository productRepository;
        private readonly ISaleOrderDetailRepository saleOrderDetailrepository;
        private readonly ICartItemRepository cartItemrepository;
        private readonly IUnitOfWork unitOfWork;

        public SaleOrderService()
        {
            var db = new DbFactory();
            repository = new SaleOrderRepository(db);
            unitOfWork = new UnitOfWork(db);
            productRepository = new ProductRepository(db);
            saleOrderDetailrepository = new SaleOrderDetailRepository(db);
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
                            Description = string.Format("Article {0} no longer exists.", item.ProductId)
                        }, new FaultReason("Error when trying to create."));
                    //ARTICULO TIENE STOCK
                    if (product.Stock < item.Quantity)
                        throw new FaultException<GeneralException>(new GeneralException()
                        {
                            Id = "BO-C-02",
                            Description = string.Format("The article {0} has sufficient stock.", item.ProductId)
                        }, new FaultReason("Error when trying to create."));
                    product.Stock -= item.Quantity;
                    products.Add(product);
                }
                //CREO LA ORDEN
                var docNumber = repository.GetAll().Max();
                myObject.DocumentNumber = docNumber == null ? ("1").PadLeft(10, '0') : docNumber.ToString().PadLeft(10, '0');
                repository.Add(myObject);
                //ACTUALIZO PRODUCTOS
                foreach (var item in products)
                    productRepository.Update(item);
                //VACIO CARRITO DE COMPRAS
                cartItemrepository.Delete(c => c.CustomerId.Equals(myObject.CustomerId));
                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                SqlException sqlException = null;
                var tmp = ex;
                while (sqlException == null && tmp != null)
                {
                    if (tmp == null) continue;
                    sqlException = tmp.InnerException as SqlException;
                    tmp = tmp.InnerException;
                }
                if (sqlException != null)
                {
                    if (sqlException.Number.Equals(2601))
                    {
                        throw new FaultException<GeneralException>(new GeneralException()
                        {
                            Id = sqlException.Number.ToString(),
                            Description = string.Format("Cannot insert duplicate value. The duplicate key value is: {0}", sqlException.Message.Split('(', ')')[1])
                        }, new FaultReason("Error when trying to create."));
                    }
                    else
                    {
                        throw new FaultException<GeneralException>(new GeneralException()
                        {
                            Id = sqlException.Number.ToString(),
                            Description = sqlException.Message
                        }, new FaultReason("Error when trying to create."));
                    }
                }
            }
        }
    }
}
