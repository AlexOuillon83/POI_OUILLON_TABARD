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

        public SeedData(
            ApplicationDbContext context,
            ILogger<SeedData> logger,
            ICityRepository cityRepository,
            IPersonRepository personRepository,
            ICommuneRepository communeRepository,
            ICategorieRepository categorieRepository)
        {
            _context = context;
            _logger = logger;
            _cityRepository = cityRepository;
            _personRepository = personRepository;
            _communeRepository = communeRepository;
            _categorieRepository = categorieRepository;
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
    }
}