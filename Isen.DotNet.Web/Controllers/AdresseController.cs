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
    public class AdresseController : BaseController<Adresse>
    {
        private readonly ICommuneRepository _communeRepo;
        private readonly IDepartementRepository _departementRepo;

        public AdresseController(
            ILogger<AdresseController> logger,
            IAdresseRepository repository,
            IDepartementRepository departementRepo,
            ICommuneRepository communeRepo)
            : base(logger, repository)
        {
            _communeRepo = communeRepo;
            _departementRepo = departementRepo;
        }

        [HttpPost]
        public override IActionResult Detail(Adresse model)
        {
            _repository.Update(model);
            _repository.Save();
            return RedirectToAction("Index");
        }
        
        /* 
        * Controller d'ajout et d'édition de l'adresse
        */
        [HttpPost]
        public IActionResult Ajout(int Id, string Texte, string ZipCode, float Longitude, float Latitude, int Commune, int Departement)
        {
            // Création d'une nouvelle adresse si elle n'existe pas
            Adresse model = _repository.Single(Id);
            if (model == null) model = new Adresse();
            
            // Hydratation des champs de l'adresse
            model.Texte = Texte;
            model.ZipCode = ZipCode;
            model.Longitude = Longitude;
            model.Latitude = Latitude;
            Commune commune = _communeRepo.Single(Commune);
            Departement departement = _departementRepo.Single(Departement);
            model.Departement = departement;
            model.Commune = commune;


            // Affichage de l'adresse à ajouter
            _logger.LogWarning(model.ToString());

            _repository.Update(model);
            _repository.Save();
            return RedirectToAction("Index");
        }

        public override IActionResult Detail(int? id){
            // Récupération de la liste des communes
            ViewBag.Communes = _communeRepo.GetAll();
            ViewBag.Departements = _departementRepo.GetAll(); 
            _logger.LogWarning("Controller de formulaire d'ajout");

            // Pas d'id > form vide (création)
            if (id == null){
                return View();
            }
            // Récupére l'adresse et la passer à la vue
            var model = _repository.Single(id.Value);
            return View(model);
        }
    }
}