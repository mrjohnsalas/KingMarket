using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KingMarket.Data.Infrastructure;
using KingMarket.Model.Models;

namespace KingMarket.Data.Repositories
{
    public class ProductTypeRepository : RepositoryBase<ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

    public interface IProductTypeRepository : IRepository<ProductType> { }
}