using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KingMarket.Model.Models;

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmployeeService" in both code and config file together.
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        IEnumerable<Employee> GetEmployees();

        [OperationContract]
        Employee GetEmployee(int id);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void CreateEmployee(Employee myObject);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void EditEmployee(Employee myObject);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void DeleteEmployee(int id);
    }
}
