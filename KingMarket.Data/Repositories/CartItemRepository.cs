using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KingMarket.Data.Infrastructure;
using KingMarket.Model.Models;

namespace KingMarket.Data.Repositories
{
    public class CartItemRepository : RepositoryBase<CartItem>, ICartItemRepository
    {
        public CartItemRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

    public interface ICartItemRepository : IRepository<CartItem> { }
}