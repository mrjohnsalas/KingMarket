using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KingMarket.Model.Models;

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProductPhotoService" in both code and config file together.
    [ServiceContract]
    public interface IProductPhotoService
    {
        [OperationContract]
        IEnumerable<ProductPhoto> GetProductPhotosByProductId(int productId);

        [OperationContract]
        ProductPhoto GetProductPhoto(int id);

        [OperationContract]
        void DeleteProductPhotosByProductId(int productId);

        [OperationContract]
        void DeleteProductPhoto(int id);
    }
}