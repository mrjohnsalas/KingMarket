using System;
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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BuyOrderDetailService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BuyOrderDetailService.svc or BuyOrderDetailService.svc.cs at the Solution Explorer and start debugging.
    public class BuyOrderDetailService : IBuyOrderDetailService
    {
        private readonly IBuyOrderDetailRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public BuyOrderDetailService()
        {
            var db = new DbFactory();
            repository = new BuyOrderDetailRepository(db);
            unitOfWork = new UnitOfWork(db);
        }
    
        public IEnumerable<BuyOrderDetail> GetBuyOrderDetailsByBuyOrderId(int buyOrderId)
        {
            return repository.GetMany(c => c.BuyOrderId.Equals(buyOrderId));
        }

        public BuyOrderDetail GetBuyOrderDetail(int id)
        {
            var myObject = repository.GetById(id);
            return myObject;
        }
    }
}
