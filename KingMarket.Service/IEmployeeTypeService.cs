﻿using System;
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

        [OperationContract]
        void CreateEmployeeType(EmployeeType myObject);

        [OperationContract]
        void EditEmployeeType(EmployeeType myObject);

        [OperationContract]
        void DeleteEmployeeType(int id);
    }
}
