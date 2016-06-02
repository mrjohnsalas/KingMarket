using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KingMarket.Model.Models;

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmployeeTypeService" in both code and config file together.
    [ServiceContract]
    public interface IEmployeeTypeService
    {
        [OperationContract]
        IEnumerable<EmployeeType> GetEmployeeTypes();

        [OperationContract]
        EmployeeType GetEmployeeType(int id);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void CreateEmployeeType(EmployeeType myObject);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void EditEmployeeType(EmployeeType myObject);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void DeleteEmployeeType(int id);
    }
}
