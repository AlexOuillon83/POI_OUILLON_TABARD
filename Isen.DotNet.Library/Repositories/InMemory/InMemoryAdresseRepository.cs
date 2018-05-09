using System.Collections.Generic;
using System.Linq;
using Isen.DotNet.Library.Models.Implementation;
using Isen.DotNet.Library.Repositories.Base;
using Isen.DotNet.Library.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Isen.DotNet.Library.Repositories.InMemory
{
    public class InMemoryAdresseRepository : BaseInMemoryRepository<Adresse>, IAdresseRepository
    {
        public InMemoryAdresseRepository(
            ILogger<InMemoryAdresseRepository> logger) : base(logger)
        {
        }

        public override IQueryable<Adresse> ModelCollection
        {
            get
            {
                if (_modelCollection == null)
                {
                    _modelCollection = new List<Adresse>
                    {
                        new Adresse { Id = 1, Nom = "83000, Toulon" },
                        new Adresse { Id = 2, Nom = "06000, Marseille" },
                        new Adresse { Id = 3, Nom = "06100, Nice" },
                    };
                }
                return _modelCollection.AsQueryable();
            }
        }

        
    }
}