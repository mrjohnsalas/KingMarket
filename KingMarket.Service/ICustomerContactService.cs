using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KingMarket.Model.Models;

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICustomerContactService" in both code and config file together.
    [ServiceContract]
    public interface ICustomerContactService
    {
        [OperationContract]
        IEnumerable<CustomerContact> GetCustomerContacts();

        [OperationContract]
        CustomerContact GetCustomerContact(int id);

        [OperationContract]
        void CreateCustomerContact(CustomerContact myObject);

        [OperationContract]
        void EditCustomerContact(CustomerContact myObject);

        [OperationContract]
        void DeleteCustomerContact(int id);
    }
}