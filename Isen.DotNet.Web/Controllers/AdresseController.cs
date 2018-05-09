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

        public AdresseController(
            ILogger<AdresseController> logger,
            IAdresseRepository repository,
            ICommuneRepository communeRepo)
            : base(logger, repository)
        {
            _communeRepo = communeRepo;
        }

        [HttpPost]
        public override IActionResult Detail(Adresse model)
        {
            _logger.LogWarning("Controller de soumission");
            _logger.LogWarning(model.ToString());
            // J'arrive pas à retrouver au moins la valeur de Commune envoyée par le formulaire
            // il manque juste ça pour, et on a gagné
            //_logger.LogWarning(String.Format("Commune: {0}", model.Commune.Nom));

            _repository.Update(model);
            _repository.Save();
            return RedirectToAction("Index");
        }

        /* 
        [HttpPost]
        public IActionResult Detail(string Texte, string ZipCode, float Longitude, float Latitude, int Commune)
        {
            _logger.LogWarning("Controller de soumission avec commune");
            _logger.LogWarning(String.Format("Commune: {0}", Commune));

            Adresse model = new Adresse();
            
            model.ZipCode = ZipCode;
            model.Longitude = Longitude;
            model.Latitude = Latitude;
            Commune commune = _communeRepo.Single(Commune);
            model.Commune = commune;

            _logger.LogWarning(model.ToString());

            _repository.Update(model);
            _repository.Save();
            return RedirectToAction("Index");
        }
        */

        public override IActionResult Detail(int? id){
            // Récupération de la liste des communes
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