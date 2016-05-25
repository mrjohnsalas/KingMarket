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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EmployeeService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EmployeeService.svc or EmployeeService.svc.cs at the Solution Explorer and start debugging.
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public EmployeeService()
        {
            var db = new DbFactory();
            repository = new EmployeeRepository(db);
            unitOfWork = new UnitOfWork(db);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return repository.GetAll();
        }

        public Employee GetEmployee(int id)
        {
            var myObject = repository.GetById(id);
            return myObject;
        }

        public void CreateEmployee(Employee myObject)
        {
            repository.Add(myObject);
            unitOfWork.Commit();
        }

        public void EditEmployee(Employee myObject)
        {
            repository.Update(myObject);
            unitOfWork.Commit();
        }

        public void DeleteEmployee(int id)
        {
            var myObject = repository.GetById(id);
            repository.Delete(myObject);
            unitOfWork.Commit();
        }
    }
}