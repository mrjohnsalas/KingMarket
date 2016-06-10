using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KingMarket.Model.Models;

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICartItemService" in both code and config file together.
    [ServiceContract]
    public interface ICartItemService
    {
        [OperationContract]
        IEnumerable<CartItem> GetCartItemsByCustomerId(int customerId);

        [OperationContract]
        CartItem GetCartItem(int id);

        [OperationContract]
        CartItem GetCartItemByProductIdAndCustomerId(int productId, int customerId);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void CreateCartItem(CartItem myObject);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void EditCartItem(CartItem myObject);

        [OperationContract]
        void DeleteCartItem(int id);
    }
}
