using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KingMarket.Data.Infrastructure;
using KingMarket.Data.Repositories;
using KingMarket.Model.Models;

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

        public IEnumerable<CartItem> GetCartItemsByUserId(string userId)
        {
            return repository.GetMany(c => c.UserId.Equals(userId));
        }

        public CartItem GetCartItem(int id)
        {
            var myObject = repository.GetById(id);
            return myObject;
        }

        public CartItem GetCartItemByProductIdAndUserId(int productId, string userId)
        {
            var myObject = repository.Get(c => c.ProductId.Equals(productId) && c.UserId.Equals(userId));
            return myObject;
        }

        public void CreateCartItem(CartItem myObject)
        {
            repository.Add(myObject);
            unitOfWork.Commit();
        }

        public void EditCartItem(CartItem myObject)
        {
            repository.Update(myObject);
            unitOfWork.Commit();
        }

        public void DeleteCartItem(int id)
        {
            var myObject = repository.GetById(id);
            repository.Delete(myObject);
            unitOfWork.Commit();
        }
    }
}
