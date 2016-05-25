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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProductService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ProductService.svc or ProductService.svc.cs at the Solution Explorer and start debugging.
    public class ProductService : IProductService
    {
        private readonly IProductRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public ProductService()
        {
            var db = new DbFactory();
            repository = new ProductRepository(db);
            unitOfWork = new UnitOfWork(db);
        }

        public IEnumerable<Product> GetProducts()
        {
            return repository.GetAll();
        }

        public Product GetProduct(int id)
        {
            var myObject = repository.GetById(id);
            return myObject;
        }

        public void CreateProduct(Product myObject)
        {
            repository.Add(myObject);
            unitOfWork.Commit();
        }

        public void EditProduct(Product myObject)
        {
            repository.Update(myObject);
            unitOfWork.Commit();
        }

        public void DeleteProduct(int id)
        {
            var myObject = repository.GetById(id);
            repository.Delete(myObject);
            unitOfWork.Commit();
        }
    }
}