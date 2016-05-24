using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KingMarket.Model.Models;

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDocumentTypeService" in both code and config file together.
    [ServiceContract]
    public interface IDocumentTypeService
    {
        [OperationContract]
        IEnumerable<DocumentType> GetDocumentTypes();

        [OperationContract]
        DocumentType GetDocumentType(int id);

        [OperationContract]
        void CreateDocumentType(DocumentType myObject);

        [OperationContract]
        void EditDocumentType(DocumentType myObject);

        [OperationContract]
        void DeleteDocumentType(int id);
    }
}
