using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KingMarket.Model.Models;

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBuyOrderService" in both code and config file together.
    [ServiceContract]
    public interface IBuyOrderService
    {
        [OperationContract]
        IEnumerable<BuyOrder> GetBuyOrdersByUserId(string userId);

        [OperationContract]
        IEnumerable<BuyOrder> GetBuyOrdersBySupplierId(int supplierId);

        [OperationContract]
        BuyOrder GetBuyOrder(int id);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void CreateBuyOrder(BuyOrder myObject);
    }
}
