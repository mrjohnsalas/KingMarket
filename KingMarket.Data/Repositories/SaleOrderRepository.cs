using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KingMarket.Data.Infrastructure;
using KingMarket.Model.Models;

namespace KingMarket.Data.Repositories
{
    public class SaleOrderRepository : RepositoryBase<SaleOrder>, ISaleOrderRepository
    {
        public SaleOrderRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

    public interface ISaleOrderRepository : IRepository<SaleOrder> { }
}