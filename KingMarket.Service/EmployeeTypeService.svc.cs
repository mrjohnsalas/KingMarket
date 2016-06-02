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

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EmployeeTypeService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EmployeeTypeService.svc or EmployeeTypeService.svc.cs at the Solution Explorer and start debugging.
    public class EmployeeTypeService : IEmployeeTypeService
    {
        private readonly IEmployeeTypeRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public EmployeeTypeService()
        {
            var db = new DbFactory();
            repository = new EmployeeTypeRepository(db);
            unitOfWork = new UnitOfWork(db);
        }

        public IEnumerable<EmployeeType> GetEmployeeTypes()
        {
            return repository.GetAll();
        }

        public EmployeeType GetEmployeeType(int id)
        {
            var myObject = repository.GetById(id);
            return myObject;
        }

        public void CreateEmployeeType(EmployeeType myObject)
        {
            try
            {
                repository.Add(myObject);
                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                SqlException sqlException = null;
                var tmp = ex;
                while (sqlException == null && tmp != null)
                {
                    if (tmp == null) continue;
                    sqlException = tmp.InnerException as SqlException;
                    tmp = tmp.InnerException;
                }
                if (sqlException != null)
                {
                    if (sqlException.Number.Equals(2601))
                    {
                        throw new FaultException<GeneralException>(new GeneralException()
                        {
                            Id = sqlException.Number.ToString(),
                            Description = string.Format("Cannot insert duplicate value. The duplicate key value is: {0}", sqlException.Message.Split('(', ')')[1])
                        }, new FaultReason("Error when trying to create."));
                    }
                    else
                    {
                        throw new FaultException<GeneralException>(new GeneralException()
                        {
                            Id = sqlException.Number.ToString(),
                            Description = sqlException.Message
                        }, new FaultReason("Error when trying to create."));
                    }
                }
            }
        }

        public void EditEmployeeType(EmployeeType myObject)
        {
            try
            {
                repository.Update(myObject);
                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                SqlException sqlException = null;
                var tmp = ex;
                while (sqlException == null && tmp != null)
                {
                    if (tmp == null) continue;
                    sqlException = tmp.InnerException as SqlException;
                    tmp = tmp.InnerException;
                }
                if (sqlException != null)
                {
                    if (sqlException.Number.Equals(2601))
                    {
                        throw new FaultException<GeneralException>(new GeneralException()
                        {
                            Id = sqlException.Number.ToString(),
                            Description = string.Format("Cannot insert duplicate value. The duplicate key value is: {0}", sqlException.Message.Split('(', ')')[1])
                        }, new FaultReason("Error when trying to edit."));
                    }
                    else
                    {
                        throw new FaultException<GeneralException>(new GeneralException()
                        {
                            Id = sqlException.Number.ToString(),
                            Description = sqlException.Message
                        }, new FaultReason("Error when trying to edit."));
                    }
                }
            }
        }

        public void DeleteEmployeeType(int id)
        {
            try
            {
                var myObject = repository.GetById(id);
                repository.Delete(myObject);
                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                SqlException sqlException = null;
                var tmp = ex;
                while (sqlException == null && tmp != null)
                {
                    if (tmp == null) continue;
                    sqlException = tmp.InnerException as SqlException;
                    tmp = tmp.InnerException;
                }
                if (sqlException != null)
                {
                    if (sqlException.Number.Equals(547))
                    {
                        throw new FaultException<GeneralException>(new GeneralException()
                        {
                            Id = sqlException.Number.ToString(),
                            Description = "Can not be deleted, there are records referenced."
                        }, new FaultReason("Error when trying to delete."));
                    }
                    else
                    {
                        throw new FaultException<GeneralException>(new GeneralException()
                        {
                            Id = sqlException.Number.ToString(),
                            Description = sqlException.Message
                        }, new FaultReason("Error when trying to delete."));
                    }
                }
            }
        }
    }
}