using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KingMarket.Data.Infrastructure;
using KingMarket.Model.Models;

namespace KingMarket.Data.Repositories
{
    public class BuyOrderDetailRepository : RepositoryBase<BuyOrderDetail>, IBuyOrderDetailRepository
    {
        public BuyOrderDetailRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

    public interface IBuyOrderDetailRepository : IRepository<BuyOrderDetail> { }
}