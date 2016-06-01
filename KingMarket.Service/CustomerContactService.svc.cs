using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using KingMarket.Data.Infrastructure;
using KingMarket.Data.Repositories;
using KingMarket.Model.Models;

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CustomerContactService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CustomerContactService.svc or CustomerContactService.svc.cs at the Solution Explorer and start debugging.
    public class CustomerContactService : ICustomerContactService
    {
        private readonly ICustomerContactRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public CustomerContactService()
        {
            var db = new DbFactory();
            repository = new CustomerContactRepository(db);
            unitOfWork = new UnitOfWork(db);
        }

        public IEnumerable<CustomerContact> GetCustomerContacts()
        {
            return repository.GetAll();
        }

        public CustomerContact GetCustomerContact(string id)
        {
            var myObject = repository.GetById(int.Parse(id));
            return myObject;
        }

        public void CreateCustomerContact(CustomerContact myObject)
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
                        throw new WebFaultException<GeneralException>(new GeneralException()
                        {
                            Id = sqlException.Number.ToString(),
                            Description = string.Format("Cannot insert duplicate value. The duplicate key value is: {0}", sqlException.Message.Split('(', ')')[1])
                        }, HttpStatusCode.Conflict);
                        //throw new FaultException<GeneralException>(new GeneralException()
                        //{
                        //    Id = sqlException.Number.ToString(),
                        //    Description = string.Format("Cannot insert duplicate value. The duplicate key value is: {0}", sqlException.Message.Split('(', ')')[1])
                        //}, new FaultReason("Error when trying to create."));
                    }
                    else
                    {
                        throw new WebFaultException<GeneralException>(new GeneralException()
                        {
                            Id = sqlException.Number.ToString(),
                            Description = sqlException.Message
                        }, HttpStatusCode.Conflict);
                        //throw new FaultException<GeneralException>(new GeneralException()
                        //{
                        //    Id = sqlException.Number.ToString(),
                        //    Description = sqlException.Message
                        //}, new FaultReason("Error when trying to create."));
                    }
                }
            }
        }

        public void EditCustomerContact(CustomerContact myObject)
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
                        throw new WebFaultException<GeneralException>(new GeneralException()
                        {
                            Id = sqlException.Number.ToString(),
                            Description = string.Format("Cannot insert duplicate value. The duplicate key value is: {0}", sqlException.Message.Split('(', ')')[1])
                        }, HttpStatusCode.NotModified);
                        //throw new FaultException<GeneralException>(new GeneralException()
                        //{
                        //    Id = sqlException.Number.ToString(),
                        //    Description = string.Format("Cannot insert duplicate value. The duplicate key value is: {0}", sqlException.Message.Split('(', ')')[1])
                        //}, new FaultReason("Error when trying to edit."));
                    }
                    else
                    {
                        throw new WebFaultException<GeneralException>(new GeneralException()
                        {
                            Id = sqlException.Number.ToString(),
                            Description = sqlException.Message
                        }, HttpStatusCode.NotModified);
                        //throw new FaultException<GeneralException>(new GeneralException()
                        //{
                        //    Id = sqlException.Number.ToString(),
                        //    Description = sqlException.Message
                        //}, new FaultReason("Error when trying to edit."));
                    }
                }
            }
        }

        public void DeleteCustomerContact(string id)
        {
            var myObject = repository.GetById(int.Parse(id));
            repository.Delete(myObject);
            unitOfWork.Commit();
        }
    }
}