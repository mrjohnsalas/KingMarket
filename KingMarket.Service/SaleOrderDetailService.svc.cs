﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KingMarket.Data.Infrastructure;
using KingMarket.Data.Repositories;
using KingMarket.Model.Models;

namespace KingMarket.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SaleOrderDetailService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SaleOrderDetailService.svc or SaleOrderDetailService.svc.cs at the Solution Explorer and start debugging.
    public class SaleOrderDetailService : ISaleOrderDetailService
    {
        private readonly ISaleOrderDetailRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public SaleOrderDetailService()
        {
            var db = new DbFactory();
            repository = new SaleOrderDetailRepository(db);
            unitOfWork = new UnitOfWork(db);
        }

        public IEnumerable<SaleOrderDetail> GetSaleOrderDetailsBySaleOrderId(int saleOrderId)
        {
            return repository.GetMany(c => c.SaleOrderId.Equals(saleOrderId));
        }

        public SaleOrderDetail GetSaleOrderDetail(int id)
        {
            var myObject = repository.GetById(id);
            return myObject;
        }
    }
}
