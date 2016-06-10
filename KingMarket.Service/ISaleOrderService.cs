using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KingMarket.Model.Models;

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISaleOrderService" in both code and config file together.
    [ServiceContract]
    public interface ISaleOrderService
    {
        [OperationContract]
        IEnumerable<SaleOrder> GetSaleOrdersByCustomerId(int customerId);

        [OperationContract]
        SaleOrder GetSaleOrder(int id);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void CreateSaleOrder(SaleOrder myObject);
    }
}
