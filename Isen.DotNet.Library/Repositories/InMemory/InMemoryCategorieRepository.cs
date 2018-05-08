using System.Collections.Generic;
using System.Linq;
using Isen.DotNet.Library.Models.Implementation;
using Isen.DotNet.Library.Repositories.Base;
using Isen.DotNet.Library.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Isen.DotNet.Library.Repositories.InMemory
{
    public class InMemoryCategorieRepository : BaseInMemoryRepository<Categorie>, ICategorieRepository
    {
        public InMemoryCategorieRepository(
            ILogger<InMemoryCategorieRepository> logger) : base(logger)
        {
        }

        public override IQueryable<Categorie> ModelCollection
        {
            get
            {
                if (_modelCollection == null)
                {
                    _modelCollection = new List<Categorie>
                    {
                        new Categorie { Id = 1, Name = "Restauration" },
                        new Categorie { Id = 2, Name = "Bars" },
                    };
                }
                return _modelCollection.AsQueryable();
            }
        }


    }
}