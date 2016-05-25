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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SupplierService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SupplierService.svc or SupplierService.svc.cs at the Solution Explorer and start debugging.
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public SupplierService()
        {
            var db = new DbFactory();
            repository = new SupplierRepository(db);
            unitOfWork = new UnitOfWork(db);
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            return repository.GetAll();
        }

        public Supplier GetSupplier(int id)
        {
            var myObject = repository.GetById(id);
            return myObject;
        }

        public void CreateSupplier(Supplier myObject)
        {
            repository.Add(myObject);
            unitOfWork.Commit();
        }

        public void EditSupplier(Supplier myObject)
        {
            repository.Update(myObject);
            unitOfWork.Commit();
        }

        public void DeleteSupplier(int id)
        {
            var myObject = repository.GetById(id);
            repository.Delete(myObject);
            unitOfWork.Commit();
        }
    }
}