using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KingMarket.Model.Models;

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProductTypeService" in both code and config file together.
    [ServiceContract]
    public interface IProductTypeService
    {
        [OperationContract]
        IEnumerable<ProductType> GetProductTypes();

        [OperationContract]
        ProductType GetProductType(int id);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void CreateProductType(ProductType myObject);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void EditProductType(ProductType myObject);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void DeleteProductType(int id);
    }
}