using System.Collections.Generic;
using System.Linq;
using Isen.DotNet.Library.Models.Implementation;
using Isen.DotNet.Library.Repositories.Base;
using Isen.DotNet.Library.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Isen.DotNet.Library.Repositories.InMemory
{
    public class InMemoryCommuneRepository : BaseInMemoryRepository<Commune>, ICommuneRepository
    {
        public InMemoryCommuneRepository(
            ILogger<InMemoryCommuneRepository> logger) : base(logger)
        {
        }

        public override IQueryable<Commune> ModelCollection
        {
            get
            {
                if (_modelCollection == null)
                {
                    _modelCollection = new List<Commune>
                    {
                        new Commune { Id = 1, Name = "Toulon" },
                        new Commune { Id = 2, Name = "Marseille" },
                        new Commune { Id = 3, Name = "Nice" },
                        new Commune { Id = 4, Name = "Paris" },
                        new Commune { Id = 5, Name = "Epinal" }
                    };
                }
                return _modelCollection.AsQueryable();
            }
        }


    }
}