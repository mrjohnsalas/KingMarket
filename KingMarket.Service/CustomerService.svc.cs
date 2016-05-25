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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CustomerService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CustomerService.svc or CustomerService.svc.cs at the Solution Explorer and start debugging.
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public CustomerService()
        {
            var db = new DbFactory();
            repository = new CustomerRepository(db);
            unitOfWork = new UnitOfWork(db);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return repository.GetAll();
        }

        public Customer GetCustomer(int id)
        {
            var myObject = repository.GetById(id);
            return myObject;
        }

        public void CreateCustomer(Customer myObject)
        {
            repository.Add(myObject);
            unitOfWork.Commit();
        }

        public void EditCustomer(Customer myObject)
        {
            repository.Update(myObject);
            unitOfWork.Commit();
        }

        public void DeleteCustomer(int id)
        {
            var myObject = repository.GetById(id);
            repository.Delete(myObject);
            unitOfWork.Commit();
        }
    }
}