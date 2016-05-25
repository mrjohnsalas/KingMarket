﻿using System;
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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SupplierContactService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SupplierContactService.svc or SupplierContactService.svc.cs at the Solution Explorer and start debugging.
    public class SupplierContactService : ISupplierContactService
    {
        private readonly ISupplierContactRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public SupplierContactService()
        {
            var db = new DbFactory();
            repository = new SupplierContactRepository(db);
            unitOfWork = new UnitOfWork(db);
        }

        public IEnumerable<SupplierContact> GetSupplierContacts()
        {
            return repository.GetAll();
        }

        public SupplierContact GetSupplierContact(int id)
        {
            var myObject = repository.GetById(id);
            return myObject;
        }

        public void CreateSupplierContact(SupplierContact myObject)
        {
            repository.Add(myObject);
            unitOfWork.Commit();
        }

        public void EditSupplierContact(SupplierContact myObject)
        {
            repository.Update(myObject);
            unitOfWork.Commit();
        }

        public void DeleteSupplierContact(int id)
        {
            var myObject = repository.GetById(id);
            repository.Delete(myObject);
            unitOfWork.Commit();
        }
    }
}