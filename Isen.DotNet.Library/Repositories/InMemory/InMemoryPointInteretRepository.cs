using System.Collections.Generic;
using System.Linq;
using Isen.DotNet.Library.Models.Implementation;
using Isen.DotNet.Library.Repositories.Base;
using Isen.DotNet.Library.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Isen.DotNet.Library.Repositories.InMemory
{
    public class InMemoryPointInteretRepository : BaseInMemoryRepository<PointInteret>, IPointInteretRepository
    {
        public InMemoryPointInteretRepository(
            ILogger<InMemoryPointInteretRepository> logger) : base(logger)
        {
        }

        public override IQueryable<PointInteret> ModelCollection
        {
            get
            {
                if (_modelCollection == null)
                {
                    _modelCollection = new List<PointInteret>
                    {
                        new PointInteret { Id = 1, Name = "Toulon"},
                        new PointInteret { Id = 2, Name = "Marseille" },
                        new PointInteret { Id = 3, Name = "Nice" },
                        new PointInteret { Id = 4, Name = "Paris" }
                    };
                }
                return _modelCollection.AsQueryable();
            }
        }  
    }
}