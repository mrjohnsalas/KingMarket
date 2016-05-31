using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KingMarket.Model.Models;

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProductService" in both code and config file together.
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        IEnumerable<Product> GetProducts();

        [OperationContract]
        Product GetProduct(int id);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void CreateProduct(Product myObject);

        [FaultContract(typeof(GeneralException))]
        [OperationContract]
        void EditProduct(Product myObject);

        [OperationContract]
        void DeleteProduct(int id);
    }
}