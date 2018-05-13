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
                        new Departement { Name = "Toulon" },
                        new Departement { Name = "Marseille" },
                        new Departement { Name = "Nice" },
                        new Departement { Name = "Paris" },
                        new Departement { Name = "Epinal" }
                    };
                }
                return _modelCollection.AsQueryable();
            }
        }


    }
}