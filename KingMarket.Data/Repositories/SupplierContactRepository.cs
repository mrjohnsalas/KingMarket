using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KingMarket.Data.Infrastructure;
using KingMarket.Model.Models;

namespace KingMarket.Data.Repositories
{
    public class SupplierContactRepository : RepositoryBase<SupplierContact>, ISupplierContactRepository
    {
        public SupplierContactRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

    public interface ISupplierContactRepository : IRepository<SupplierContact> { }
}