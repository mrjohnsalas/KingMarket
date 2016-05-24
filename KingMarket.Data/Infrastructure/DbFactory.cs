using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KingMarket.Data.Context;

namespace KingMarket.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        KingMarketContext dbContext;

        public KingMarketContext Init()
        {
            return dbContext ?? (dbContext = new KingMarketContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
