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
            repository.Add(myObject);
            unitOfWork.Commit();
        }

        public void EditEmployeeType(EmployeeType myObject)
        {
            repository.Update(myObject);
            unitOfWork.Commit();
        }

        public void DeleteEmployeeType(int id)
        {
            var myObject = repository.GetById(id);
            repository.Delete(myObject);
            unitOfWork.Commit();
        }
    }
}