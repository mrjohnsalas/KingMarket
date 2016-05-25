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

        public CustomerContact GetCustomerContact(int id)
        {
            var myObject = repository.GetById(id);
            return myObject;
        }

        public void CreateCustomerContact(CustomerContact myObject)
        {
            repository.Add(myObject);
            unitOfWork.Commit();
        }

        public void EditCustomerContact(CustomerContact myObject)
        {
            repository.Update(myObject);
            unitOfWork.Commit();
        }

        public void DeleteCustomerContact(int id)
        {
            var myObject = repository.GetById(id);
            repository.Delete(myObject);
            unitOfWork.Commit();
        }
    }
}