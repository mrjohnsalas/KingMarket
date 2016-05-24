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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProductTypeService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ProductTypeService.svc or ProductTypeService.svc.cs at the Solution Explorer and start debugging.
    public class ProductTypeService : IProductTypeService
    {
        private readonly IProductTypeRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public ProductTypeService()
        {
            var db = new DbFactory();
            repository = new ProductTypeRepository(db);
            unitOfWork = new UnitOfWork(db);
        }

        public IEnumerable<ProductType> GetProductTypes()
        {
            return repository.GetAll();
        }

        public ProductType GetProductType(int id)
        {
            var myObject = repository.GetById(id);
            return myObject;
        }

        public void CreateProductType(ProductType myObject)
        {
            repository.Add(myObject);
            unitOfWork.Commit();
        }

        public void EditProductType(ProductType myObject)
        {
            repository.Update(myObject);
            unitOfWork.Commit();
        }

        public void DeleteProductType(int id)
        {
            var myObject = repository.GetById(id);
            repository.Delete(myObject);
            unitOfWork.Commit();
        }
    }
}