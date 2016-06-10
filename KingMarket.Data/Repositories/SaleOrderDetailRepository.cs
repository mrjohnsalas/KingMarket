using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KingMarket.Data.Infrastructure;
using KingMarket.Model.Models;

namespace KingMarket.Data.Repositories
{
    public class SaleOrderDetailRepository : RepositoryBase<SaleOrderDetail>, ISaleOrderDetailRepository
    {
        public SaleOrderDetailRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

    public interface ISaleOrderDetailRepository : IRepository<SaleOrderDetail> { }
}