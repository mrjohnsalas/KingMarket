using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KingMarket.Model.Models;

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISupplierContactService" in both code and config file together.
    [ServiceContract]
    public interface ISupplierContactService
    {
        [OperationContract]
        IEnumerable<SupplierContact> GetSupplierContacts();

        [OperationContract]
        SupplierContact GetSupplierContact(int id);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void CreateSupplierContact(SupplierContact myObject);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void EditSupplierContact(SupplierContact myObject);

        [OperationContract]
        void DeleteSupplierContact(int id);
    }
}
