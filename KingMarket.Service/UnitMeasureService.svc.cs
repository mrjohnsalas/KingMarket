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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UnitMeasureService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UnitMeasureService.svc or UnitMeasureService.svc.cs at the Solution Explorer and start debugging.
    public class UnitMeasureService : IUnitMeasureService
    {
        private readonly IUnitMeasureRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public UnitMeasureService()
        {
            var db = new DbFactory();
            repository = new UnitMeasureRepository(db);
            unitOfWork = new UnitOfWork(db);
        }

        public IEnumerable<UnitMeasure> GetUnitMeasures()
        {
            return repository.GetAll();
        }

        public UnitMeasure GetUnitMeasure(int id)
        {
            var myObject = repository.GetById(id);
            return myObject;
        }

        public void CreateUnitMeasure(UnitMeasure myObject)
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

        public void EditUnitMeasure(UnitMeasure myObject)
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

        public void DeleteUnitMeasure(int id)
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
