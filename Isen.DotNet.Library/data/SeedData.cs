using System;
using System.Collections.Generic;
using System.Linq;
using Isen.DotNet.Library.Models.Implementation;
using Isen.DotNet.Library.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Isen.DotNet.Library.Data
{
    public class SeedData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SeedData> _logger;
        private readonly ICityRepository _cityRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ICommuneRepository _communeRepository;
        private readonly ICategorieRepository _categorieRepository;
        private readonly IAdresseRepository _adresseRepository;

        private readonly IPointInteretRepository _pointInteretRepository;

        public SeedData(
            ApplicationDbContext context,
            ILogger<SeedData> logger,
            ICityRepository cityRepository,
            IPersonRepository personRepository,
            ICommuneRepository communeRepository,
            ICategorieRepository categorieRepository,
            IPointInteretRepository pointInteretRepository)
        {
            _context = context;
            _logger = logger;
            _cityRepository = cityRepository;
            _personRepository = personRepository;
            _communeRepository = communeRepository;
            _categorieRepository = categorieRepository;
            _pointInteretRepository = pointInteretRepository;
        }

        public void DropDatabase()
        {
            var deleted = _context.Database.EnsureDeleted();
            var not = deleted ? "" : "not ";
            _logger.LogWarning($"Database was {not}dropped.");
        }

        public void CreateDatabase()
        {
            var created = _context.Database.EnsureCreated();
            var not = created ? "" : "not ";
            _logger.LogWarning($"Database was {not}created.");
        }

        public void AddCategories(){
            if (_categorieRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding categories");

            var categories = new List<Categorie>
            {
                new Categorie { Nom = "Restauration" },
                new Categorie { Nom = "Bars" },
            };
            _categorieRepository.UpdateRange(categories);
            _categorieRepository.Save();

            _logger.LogWarning("Added categories");
        }

        public void AddAdresses(){
            if (_adresseRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding adresses");

            var adresses = new List<Adresse>
            {
                new Adresse { Texte = "14 Rue Chevalier Paul, 83000 Toulon" },
                new Adresse { Texte = "7 Rue Vernier, 06000 Nice" },
            };
            _adresseRepository.UpdateRange(adresses);
            _adresseRepository.Save();

            _logger.LogWarning("Added adresses");
        }

        public void AddCommunes(){
            if (_communeRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding communes");

            var communes = new List<Commune>
            {
                new Commune { Name = "Toulon" },
                new Commune { Name = "Marseille" },
                new Commune { Name = "Nice" },
                new Commune { Name = "Paris" },
                new Commune { Name = "Epinal" }
            };
            _communeRepository.UpdateRange(communes);
            _communeRepository.Save();

            _logger.LogWarning("Added communes");
        }

        public void AddPointInterets(){
            if (_pointInteretRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding point interet");

            var pointInterets = new List<PointInteret>
            {
                new PointInteret {
                    Name = "Toulon"              
                 },
                new PointInteret { Name = "Marseille" },
                new PointInteret { Name = "Nice" },
                new PointInteret { Name = "Paris" },
                new PointInteret { Name = "Epinal" }
            };
            _pointInteretRepository.UpdateRange(pointInterets);
            _pointInteretRepository.Save();

            _logger.LogWarning("Added point interets");
        }
    }
}