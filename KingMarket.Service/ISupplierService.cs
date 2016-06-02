﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KingMarket.Model.Models;

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISupplierService" in both code and config file together.
    [ServiceContract]
    public interface ISupplierService
    {
        [OperationContract]
        IEnumerable<Supplier> GetSuppliers();

        [OperationContract]
        Supplier GetSupplier(int id);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void CreateSupplier(Supplier myObject);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void EditSupplier(Supplier myObject);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void DeleteSupplier(int id);
    }
}