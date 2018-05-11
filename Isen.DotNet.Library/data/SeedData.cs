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

        private readonly IDepartementRepository _departementRepository;

        public SeedData(
            ApplicationDbContext context,
            ILogger<SeedData> logger,
            ICityRepository cityRepository,
            IPersonRepository personRepository,
            ICommuneRepository communeRepository,
            ICategorieRepository categorieRepository,
            IPointInteretRepository pointInteretRepository,
            IAdresseRepository adresseRepository,
            IDepartementRepository departementRepository)
        {
            _context = context;
            _logger = logger;
            _cityRepository = cityRepository;
            _personRepository = personRepository;
            _communeRepository = communeRepository;
            _categorieRepository = categorieRepository;
            _pointInteretRepository = pointInteretRepository;
            _adresseRepository = adresseRepository;
            _departementRepository = departementRepository;
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
                new Adresse { Texte = "14 Rue Chevalier Paul", ZipCode = "83000", Longitude = 145, Latitude = 333 },
                new Adresse { Texte = "19 Bd Mazarin Paul", ZipCode = "75800", Longitude = 853, Latitude = 978 }
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
                new Commune { Nom = "Toulon" },
                new Commune { Nom = "Marseille" },
                new Commune { Nom = "Nice" },
                new Commune { Nom = "Paris" },
                new Commune { Nom = "Nîmes" }
            };
            _communeRepository.UpdateRange(communes);
            _communeRepository.Save();

            _logger.LogWarning("Added communes");
        }

        public void AddDepartements(){
            if (_departementRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding departement");

            var departements = new List<Departement>
            {
                new Departement { Nom = "Var" },
                new Departement { Nom = "Bouches du Rhône" },
                new Departement { Nom = "Alpes Maritimes" },
                new Departement { Nom = "Hautes-Alpes" },
                new Departement { Nom = "Alpes de Haute-Provence" },
                new Departement { Nom = "Vaucluse" }
                
                
            };
            _departementRepository.UpdateRange(departements);
            _departementRepository.Save();

            _logger.LogWarning("Added departements");
        }

        public void AddPointInterets(){
            if (_pointInteretRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding point interet");

            var pointInterets = new List<PointInteret>
            {
                new PointInteret { 
                    Name = "Le Mourillon",
                    Adresse = new Adresse { Texte = "14 Rue Chevalier Paul", ZipCode = "83000", Longitude = 43, Latitude = 6, Commune = new Commune{Name = "Toulon"}},
                    Descriptif = "Venez le mardi, Bd de Strasbourd : KFC >>> double vé ash e ygrec",
                    },
                new PointInteret { 
                    Name = "Vieux-port",
                    Adresse = new Adresse {Texte = " Vieux-port", ZipCode = "13000", Longitude = 43 , Latitude = 5, Commune = new Commune{Name = "Marseille"}},
                    Descriptif = "Ville sportive, avec un joli port",
                    }
            };
            _pointInteretRepository.UpdateRange(pointInterets);
            _pointInteretRepository.Save();

            _logger.LogWarning("Added point interets");
        }

        public void getListeDepartement(){
            //Département est une classe qui s'avère être une liste de commune ayant le même début de code postal
            //A créer
        }

        public void getListeCommunesPACA(){
            //Liste de commune de la région
        }
    }
}