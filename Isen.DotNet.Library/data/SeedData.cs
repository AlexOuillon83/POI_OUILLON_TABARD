using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Isen.DotNet.Library.Models.Implementation;
using Isen.DotNet.Library.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
using CsvHelper;

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

        public void AddCategories()
        {
            if (_categorieRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding categories");

            List<Categorie> categories = getListeCategories();
            _categorieRepository.UpdateRange(categories);
            _categorieRepository.Save();

            _logger.LogWarning("Added categories");
        }

        public void AddAdresses()
        {
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

        public void AddCommunes()
        {
            if (_communeRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding communes");
            List<Commune> communes = getListeCommunesPACA();
            _communeRepository.UpdateRange(communes);
            _communeRepository.Save();

            _logger.LogWarning("Added communes");
        }

        public void AddPointInterets()
        {
            if (_pointInteretRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding point interet");

            var pointInterets = getListePointInterets();
            foreach (var item in pointInterets)
            {
                _logger.LogWarning(item.ToString());
            }
            _pointInteretRepository.UpdateRange(pointInterets);
            _pointInteretRepository.Save();

            _logger.LogWarning("Added point interets");
        }

        public void AddDepartements()
        {
            if (_departementRepository.GetAll().Any()) return;
            _logger.LogWarning("Adding departements");

            List<Departement> departements = getListeDepartement();
            foreach (var item in departements)
            {
                _logger.LogWarning(String.Format("Ajout departement: {0}", item.Nom));
            }
            _departementRepository.UpdateRange(departements);
            _departementRepository.Save();

            _logger.LogWarning("Added departements");
        }


        public List<PointInteret> getListePointInterets()
        {
            // Liste des catégories
            StreamReader textReader = File.OpenText("../Isen.Dotnet.Library/Data/poi.csv");
            var csv = new CsvReader(textReader);
            csv.Configuration.Delimiter = ",";
            List<PointInteret> pointInterets = new List<PointInteret>();
            var i = 0;
            while (csv.Read())
            {
                if (i == 0)
                {
                    i++;
                    continue;
                }

                // Récupère le nom du point d'intérêt
                string nom = csv.GetField<string>(0);
                // Récupère la description 
                string description = csv.GetField<string>(1);
                // Récupère le nom la catégorie 
                string nomCategorie = csv.GetField<string>(2);
                // Récupère le texte de l'adresse
                string texte = csv.GetField<string>(3);
                // Récupère le ZipCode
                string zipCode = csv.GetField<string>(4);
                // Récupère la longitude
                string longitude = csv.GetField<string>(5);
                // Récupère la latitude
                string latitude = csv.GetField<string>(6);
                // Récupère la commune
                string nomCommune = csv.GetField<string>(7);

                _logger.LogWarning(String.Format("{0} - {1} - {2} - {3} - {4} - {5} - {6}", nom, nomCategorie, texte, zipCode, longitude, latitude, nomCommune));

                float floatLong = -1;
                float floatLat = -1;

                try
                {
                    longitude = longitude.Replace(".", ",");
                    latitude = latitude.Replace(".", ",");
                    floatLong = float.Parse(longitude);
                    floatLat = float.Parse(latitude);
                }
                catch (System.Exception)
                {
                    _logger.LogWarning("Unable to get 'longitude' or 'latitude'");
                }

                // Création de la commune si besoin
                _logger.LogWarning(nomCommune);
                Commune commune = _communeRepository.Single(nomCommune);
                if (commune == null)
                {
                    commune = new Commune();
                    commune.Nom = nomCommune;
                    commune.Longitude = floatLong;
                    commune.Latitude = floatLat;
                }
                _communeRepository.Update(commune);
                _communeRepository.Save();

                // Création de l'adresse si besoin
                Adresse adresse = _adresseRepository.Single(texte);
                if (adresse == null)
                {
                    adresse = new Adresse();
                    adresse.Texte = texte;
                    adresse.ZipCode = zipCode;
                    adresse.Longitude = floatLong;
                    adresse.Latitude = floatLat;
                    adresse.Commune = commune;
                }
                _adresseRepository.Update(adresse);
                _adresseRepository.Save();

                // Création de la catégorie si besoin
                Categorie categorie = _categorieRepository.Single(nomCategorie);
                if (categorie == null)
                {
                    categorie = new Categorie();
                    categorie.Nom = nomCategorie;
                }
                _categorieRepository.Update(categorie);
                _categorieRepository.Save();

                // Création du point d'intérêt
                PointInteret pointInteret = _pointInteretRepository.Single(nom);
                if (pointInteret == null)
                {
                    pointInteret = new PointInteret();
                    pointInteret.Nom = nom;
                    pointInteret.Description = description;
                    pointInteret.Categorie = categorie;
                    pointInteret.Adresse = adresse;
                }

                pointInterets.Add(pointInteret);
            }
            return pointInterets;
        }

        public List<Categorie> getListeCategories()
        {
            // Liste des catégories
            string jsonpath = "../Isen.Dotnet.Library/Data/categories.json";
            string result = string.Empty;
            result = File.ReadAllText(jsonpath);
            return JsonConvert.DeserializeObject<List<Categorie>>(result);
        }
        public List<Departement> getListeDepartement()
        {
            // Liste des départements
            string jsonpath = "../Isen.Dotnet.Library/Data/departements.json";
            string result = string.Empty;
            result = File.ReadAllText(jsonpath);
            return JsonConvert.DeserializeObject<List<Departement>>(result);
        }

        public List<Commune> getListeCommunesPACA()
        {
            //Liste des communes de la région
            _logger.LogWarning("Importing 'communes' this might take a while...");
            StreamReader textReader = File.OpenText("../Isen.Dotnet.Library/Data/OpenData.csv");
            var csv = new CsvReader(textReader);
            csv.Configuration.Delimiter = ";";
            List<Commune> communes = new List<Commune>();
            while (csv.Read())
            {
                var strField = csv.GetField<string>(2);
                if (strField == "Provence-Alpes-Côte d'Azur")
                {
                    var communeField = csv.GetField<string>(8);
                    string nomDepartement = csv.GetField<string>(5);

                    string longitude = "";
                    string latitude = "";
                    float floatLat = -1;
                    float floatLong = -1;
                    try
                    {
                        latitude = csv.GetField<string>(11);
                        longitude = csv.GetField<string>(12);
                        latitude = latitude.Replace(".", ",");
                        longitude = longitude.Replace(".", ",");
                        floatLat = float.Parse(latitude);
                        floatLong = float.Parse(longitude);
                    }
                    catch (System.Exception)
                    {
                        //_logger.LogWarning("Cannot get 'longitude' or 'latitude'");
                    }

                    Commune commune = new Commune();
                    Departement departement = _departementRepository.Single(nomDepartement);
                    if (departement == null)
                    {
                        departement = new Departement();
                        departement.Nom = nomDepartement;
                        _departementRepository.Update(departement);
                        _departementRepository.Save();
                    }

                    commune.Nom = communeField;
                    commune.Longitude = floatLong;
                    commune.Latitude = floatLat;
                    commune.Departement = departement;
                    commune.DepartementId = departement.Id;
                    communes.Add(commune);

                    // _logger.LogWarning(String.Format("Commune: {0} ({1} - {2})", communeField, floatLong, floatLat));
                }
            }
            return communes;
        }
    }
}