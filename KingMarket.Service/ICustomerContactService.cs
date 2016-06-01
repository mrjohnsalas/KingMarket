using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using KingMarket.Model.Models;

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICustomerContactService" in both code and config file together.
    [ServiceContract]
    public interface ICustomerContactService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "CustomerContacts", ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<CustomerContact> GetCustomerContacts();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "CustomerContacts/{id}", ResponseFormat = WebMessageFormat.Json)]
        CustomerContact GetCustomerContact(string id);

        //[FaultContract(typeof(GeneralException))]
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "CustomerContacts", ResponseFormat = WebMessageFormat.Json)]
        void CreateCustomerContact(CustomerContact myObject);

        //[FaultContract(typeof(GeneralException))]
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "CustomerContacts", ResponseFormat = WebMessageFormat.Json)]
        void EditCustomerContact(CustomerContact myObject);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "CustomerContacts/{id}", ResponseFormat = WebMessageFormat.Json)]
        void DeleteCustomerContact(string id);
    }
}