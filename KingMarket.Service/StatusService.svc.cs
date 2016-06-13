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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "StatusService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select StatusService.svc or StatusService.svc.cs at the Solution Explorer and start debugging.
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public StatusService()
        {
            var db = new DbFactory();
            repository = new StatusRepository(db);
            unitOfWork = new UnitOfWork(db);
        }

        public IEnumerable<Status> GetStatuses()
        {
            return repository.GetAll();
        }

        public Status GetStatus(int id)
        {
            var myObject = repository.GetById(id);
            return myObject;
        }

        public void CreateStatus(Status myObject)
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

        public void EditStatus(Status myObject)
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

        public void DeleteStatus(int id)
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
