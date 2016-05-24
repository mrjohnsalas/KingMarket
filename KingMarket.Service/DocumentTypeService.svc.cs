using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KingMarket.Data.Infrastructure;
using KingMarket.Data.Repositories;
using KingMarket.Model.Models;

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DocumentTypeService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DocumentTypeService.svc or DocumentTypeService.svc.cs at the Solution Explorer and start debugging.
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly IDocumentTypeRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public DocumentTypeService()
        {
            var db = new DbFactory();
            repository = new DocumentTypeRepository(db);
            unitOfWork = new UnitOfWork(db);
        }

        public IEnumerable<DocumentType> GetDocumentTypes()
        {
            return repository.GetAll();
        }

        public DocumentType GetDocumentType(int id)
        {
            var myObject = repository.GetById(id);
            return myObject;
        }

        public void CreateDocumentType(DocumentType myObject)
        {
            repository.Add(myObject);
            unitOfWork.Commit();
        }

        public void EditDocumentType(DocumentType myObject)
        {
            repository.Update(myObject);
            unitOfWork.Commit();
        }

        public void DeleteDocumentType(int id)
        {
            var myObject = repository.GetById(id);
            repository.Delete(myObject);
            unitOfWork.Commit();
        }
    }
}