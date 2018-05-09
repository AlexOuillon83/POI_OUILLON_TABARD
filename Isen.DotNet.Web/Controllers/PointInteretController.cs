using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Isen.DotNet.Web.Models;
using Isen.DotNet.Library.Repositories.Interfaces;
using Isen.DotNet.Library.Repositories.InMemory;
using Isen.DotNet.Library.Models.Implementation;
using Isen.DotNet.Library.Repositories.DbContext;
using Microsoft.Extensions.Logging;
namespace Isen.DotNet.Web.Controllers
{
    public class PointInteretController : BaseController<PointInteret>
    {
        private readonly ICategorieRepository _categorieRepo; 
        private readonly ICommuneRepository _communeRepo;
        private IAdresseRepository _adresseRepo;

        public PointInteretController(
            ILogger<PointInteretController> logger,
            IPointInteretRepository repository,
            ICategorieRepository categorieRepo,
            ICommuneRepository communeRepo,
            IAdresseRepository adresseRepo)

            : base(logger, repository)
        {
            _categorieRepo = categorieRepo;
            _communeRepo = communeRepo;
            _adresseRepo = adresseRepo;
        }

        /* 
        * Controller d'ajout et d'édition de l'adresse
        */
        [HttpPost]
        public IActionResult Ajout(int Id, string Name, string Descriptif, string Texte, string ZipCode, float Longitude, float Latitude, int Commune, int Categorie)
        {
            // Création du poi s'il n'existe pas
            PointInteret model = _repository.Single(Id);
            if (model == null) model = new PointInteret();

            Adresse adresse = new Adresse();

            // Hydratation des champs de l'adresse
            
            adresse.Texte = Texte;
            adresse.ZipCode = ZipCode;
            adresse.Longitude = Longitude;
            adresse.Latitude = Latitude;
            Commune commune = _communeRepo.Single(Commune);
            adresse.Commune = commune;
            _adresseRepo.Update(adresse);
            _adresseRepo.Save();

            Categorie categorie = _categorieRepo.Single(Categorie);

            // Hydratation des champs du poi
            model.Name = Name;
            model.Descriptif = Descriptif;
            model.Categorie = categorie;
            model.Adresse = adresse;

            // Affichage du poi à ajouter
            _logger.LogWarning(model.ToString());

            _repository.Update(model);
            _repository.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public override IActionResult Detail(PointInteret model)
        {
            _logger.LogWarning("Controller de soumission");
            _logger.LogWarning(model.ToString());

            _repository.Update(model);
            _repository.Save();
            return RedirectToAction("Index");
        }

        public override IActionResult Detail(int? id){
            // Récupération de la liste des communes
            
            ViewBag.Categories = _categorieRepo.GetAll();
            ViewBag.Communes = _communeRepo.GetAll();

            _logger.LogWarning("Controller de formulaire d'ajout");

            // Pas d'id > form vide (création)
            if (id == null){
                return View();
            }
            // Récupérer la ville et la passer à la vue
            var model = _repository.Single(id.Value);
            return View(model);
        }
    }
}