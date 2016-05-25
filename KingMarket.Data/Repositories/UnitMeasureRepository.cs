using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KingMarket.Data.Infrastructure;
using KingMarket.Model.Models;

namespace KingMarket.Data.Repositories
{
    public class UnitMeasureRepository : RepositoryBase<UnitMeasure>, IUnitMeasureRepository
    {
        public UnitMeasureRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

    public interface IUnitMeasureRepository : IRepository<UnitMeasure> { }
}