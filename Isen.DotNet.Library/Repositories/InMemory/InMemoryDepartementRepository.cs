using System.Collections.Generic;
using System.Linq;
using Isen.DotNet.Library.Models.Implementation;
using Isen.DotNet.Library.Repositories.Base;
using Isen.DotNet.Library.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Isen.DotNet.Library.Repositories.InMemory
{
    public class InMemoryDepartementRepository : BaseInMemoryRepository<Departement>, IDepartementRepository
    {
        public InMemoryDepartementRepository(
            ILogger<InMemoryDepartementRepository> logger) : base(logger)
        {
        }

        public override IQueryable<Departement> ModelCollection
        {
            get
            {
                if (_modelCollection == null)
                {
                    _modelCollection = new List<Departement>
                    {
                        new Departement { Id = 1, Name = "Toulon" },
                        new Departement { Id = 2, Name = "Marseille" },
                        new Departement { Id = 3, Name = "Nice" },
                        new Departement { Id = 4, Name = "Paris" },
                        new Departement { Id = 5, Name = "Epinal" }
                    };
                }
                return _modelCollection.AsQueryable();
            }
        }


    }
}