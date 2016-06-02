using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KingMarket.Model.Models;

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICustomerService" in both code and config file together.
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        IEnumerable<Customer> GetCustomers();

        [OperationContract]
        Customer GetCustomer(int id);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void CreateCustomer(Customer myObject);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void EditCustomer(Customer myObject);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void DeleteCustomer(int id);
    }
}
