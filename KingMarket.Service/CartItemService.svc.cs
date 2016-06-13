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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CartItemService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CartItemService.svc or CartItemService.svc.cs at the Solution Explorer and start debugging.
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public CartItemService()
        {
            var db = new DbFactory();
            repository = new CartItemRepository(db);
            unitOfWork = new UnitOfWork(db);
        }

        public IEnumerable<CartItem> GetCartItemsByCustomerId(int customerId)
        {
            return repository.GetMany(c => c.CustomerId.Equals(customerId));
        }

        public CartItem GetCartItem(int id)
        {
            var myObject = repository.GetById(id);
            return myObject;
        }

        public CartItem GetCartItemByProductIdAndCustomerId(int productId, int customerId)
        {
            var myObject = repository.Get(c => c.ProductId.Equals(productId) && c.CustomerId.Equals(customerId));
            return myObject;
        }

        public void CreateCartItem(CartItem myObject)
        {
            try
            {
                if(myObject.Quantity.Equals(0))
                    throw new FaultException<GeneralException>(new GeneralException()
                    {
                        Id = "1",
                        Description = "Cannot insert Zero value."
                    }, new FaultReason("Error when trying to create."));
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

        public void EditCartItem(CartItem myObject)
        {
            try
            {
                if (myObject.Quantity.Equals(0))
                    throw new FaultException<GeneralException>(new GeneralException()
                    {
                        Id = "1",
                        Description = "Cannot insert Zero value."
                    }, new FaultReason("Error when trying to edit."));
                repository.Update(myObject);
                unitOfWork.Commit();
            }
            catch (FaultException<GeneralException> gException)
            {
                throw gException;
            }
            catch (Exception ex)
            {
                throw Utilities.GetException(ex, "edit.");
            }
        }

        public void DeleteCartItem(int id)
        {
            var myObject = repository.GetById(id);
            repository.Delete(myObject);
            unitOfWork.Commit();
        }
    }
}