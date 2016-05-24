using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KingMarket.Model.Models;

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IClassDocumentTypeService" in both code and config file together.
    [ServiceContract]
    public interface IClassDocumentTypeService
    {
        [OperationContract]
        IEnumerable<ClassDocumentType> GetClassDocumentTypes();

        [OperationContract]
        ClassDocumentType GetClassDocumentType(int id);

        [OperationContract]
        void CreateClassDocumentType(ClassDocumentType myObject);

        [OperationContract]
        void EditClassDocumentType(ClassDocumentType myObject);

        [OperationContract]
        void DeleteClassDocumentType(int id);
    }
}
