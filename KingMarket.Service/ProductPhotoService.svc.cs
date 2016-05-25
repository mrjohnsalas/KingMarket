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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProductPhotoService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ProductPhotoService.svc or ProductPhotoService.svc.cs at the Solution Explorer and start debugging.
    public class ProductPhotoService : IProductPhotoService
    {
        private readonly IProductPhotoRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public ProductPhotoService()
        {
            var db = new DbFactory();
            repository = new ProductPhotoRepository(db);
            unitOfWork = new UnitOfWork(db);
        }

        public ProductPhoto GetProductPhoto(int id)
        {
            var myObject = repository.GetById(id);
            return myObject;
        }
    }
}