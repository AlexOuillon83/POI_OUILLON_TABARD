using System.Collections.Generic;
using System.Linq;
using Isen.DotNet.Library.Data;
using Isen.DotNet.Library.Models.Base;
using Isen.DotNet.Library.Models.Implementation;
using Isen.DotNet.Library.Repositories.Base;
using Isen.DotNet.Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Isen.DotNet.Library.Repositories.DbContext
{
    public class DbContextPointInteretRepository :
        BaseDbContextRepository<PointInteret>, IPointInteretRepository
    {
        public DbContextPointInteretRepository(
            ILogger<DbContextPointInteretRepository> logger, 
            ApplicationDbContext context) 
            : base(logger, context)
        {
        }

        //public override IQueryable<City> Includes(
          //  IQueryable<PointInteret> queryable)
            //    => queryable.Include(c => c.Nom);
    }
}