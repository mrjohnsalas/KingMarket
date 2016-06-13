using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KingMarket.Data.Infrastructure;
using KingMarket.Data.Repositories;
using KingMarket.Model.Models;
using KingMarket.Utility;

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ClassDocumentTypeService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ClassDocumentTypeService.svc or ClassDocumentTypeService.svc.cs at the Solution Explorer and start debugging.
    public class ClassDocumentTypeService : IClassDocumentTypeService
    {
        private readonly IClassDocumentTypeRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public ClassDocumentTypeService()
        {
            var db = new DbFactory();
            repository = new ClassDocumentTypeRepository(db);
            unitOfWork = new UnitOfWork(db);
        }

        public IEnumerable<ClassDocumentType> GetClassDocumentTypes()
        {
            return repository.GetAll();
        }

        public ClassDocumentType GetClassDocumentType(int id)
        {
            var myObject = repository.GetById(id);
            return myObject;
        }

        public void CreateClassDocumentType(ClassDocumentType myObject)
        {
            try
            {
                repository.Add(myObject);
                unitOfWork.Commit();
            }
            catch (FaultException<GeneralException> gException)
            {
                throw gException;
            }
            catch (Exception ex)
            {
                throw Utilities.GetException(ex, "create.");
            }
        }

        public void EditClassDocumentType(ClassDocumentType myObject)
        {
            try
            {
                repository.Update(myObject);
                unitOfWork.Commit();
            }
            catch (FaultException<GeneralException> gException)
            {
                throw gException;
            }
            catch (Exception ex)
            {
                throw Utilities.GetException(ex, "edit.");
            }
        }

        public void DeleteClassDocumentType(int id)
        {
            try
            {
                var myObject = repository.GetById(id);
                repository.Delete(myObject);
                unitOfWork.Commit();
            }
            catch (FaultException<GeneralException> gException)
            {
                throw gException;
            }
            catch (Exception ex)
            {
                throw Utilities.GetException(ex, "delete.");
            }
        }
    }
}