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
using KingMarket.Utility;

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BuyOrderService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BuyOrderService.svc or BuyOrderService.svc.cs at the Solution Explorer and start debugging.
    public class BuyOrderService : IBuyOrderService
    {
        private readonly IBuyOrderRepository repository;
        private readonly IProductRepository productRepository;
        private readonly IBuyOrderDetailRepository buyOrderDetailrepository;
        private readonly ICartItemRepository cartItemrepository;
        private readonly IUnitOfWork unitOfWork;

        public BuyOrderService()
        {
            var db = new DbFactory();
            repository = new BuyOrderRepository(db);
            unitOfWork = new UnitOfWork(db);
            productRepository = new ProductRepository(db);
            buyOrderDetailrepository = new BuyOrderDetailRepository(db);
            cartItemrepository = new CartItemRepository(db);
        }

        public IEnumerable<BuyOrder> GetBuyOrdersByUserId(string userId)
        {
            return repository.GetMany(c => c.UserId.Equals(userId));
        }

        public IEnumerable<BuyOrder> GetBuyOrdersBySupplierId(int supplierId)
        {
            return repository.GetMany(c => c.SupplierId.Equals(supplierId));
        }

        public BuyOrder GetBuyOrder(int id)
        {
            var myObject = repository.GetById(id);
            return myObject;
        }

        public void CreateBuyOrder(BuyOrder myObject)
        {
            try
            {
                //ASIGNO FECHA ACTUAL A LA ORDEN
                myObject.DateOrder = DateTime.Now;
                //VERIFICO SI:
                foreach (var item in myObject.BuyOrderDetails)
                {
                    var product = productRepository.GetById(item.ProductId);
                    //ARTICULO EXISTE
                    if(product == null)
                        throw new FaultException<GeneralException>(new GeneralException()
                        {
                            Id = "BO-C-01",
                            Description = string.Format("Article {0} no longer exists.", item.ProductId)
                        }, new FaultReason("Error when trying to create."));
                    item.Product = null;
                    item.DeliveryDate = myObject.DateOrder;
                }
                //CREO LA ORDEN
                repository.Add(myObject);
                unitOfWork.Commit();
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
