using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KingMarket.Data.Context;

namespace KingMarket.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        KingMarketContext Init();
    }
}
