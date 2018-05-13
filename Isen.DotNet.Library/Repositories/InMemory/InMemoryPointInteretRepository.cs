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
        private IAdresseRepository _adresseRepository;
        private ICategorieRepository _categorieRepository;
        public InMemoryPointInteretRepository(
            ILogger<InMemoryPointInteretRepository> logger,
            IAdresseRepository adresseRepository,
            ICategorieRepository categorieRepository) : base(logger)
        {
                _categorieRepository = categorieRepository;
                _adresseRepository = adresseRepository;
        }

        public override IQueryable<PointInteret> ModelCollection
        {
            get
            {
                if (_modelCollection == null)
                {
                    _modelCollection = new List<PointInteret>
                    {
                        /*
                        new PointInteret { 
                            Id = 1, 
                            Name = "Toulon", 
                            Descriptif = " Salut à toi jeune padawan",
                            Adresse = _adresseRepository.Single("83 000 Boulevard Streetzer"),
                            Categorie = _categorieRepository.Single("Rugby")
                            },
                        new PointInteret { 
                            Id = 2, 
                            Name = "Marseille",
                            Descriptif = " Hello à toi aussi fitboy",
                            Adresse = _adresseRepository.Single("83 000 Boulevard Streetzer"),
                            Categorie = _categorieRepository.Single("Rugby")

                             }
                             */
                    };
                }
                return _modelCollection.AsQueryable();
            }
        }  
    }
}