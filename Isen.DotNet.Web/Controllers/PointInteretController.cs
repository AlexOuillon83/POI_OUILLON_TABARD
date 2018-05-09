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

        public PointInteretController(
            ILogger<PointInteretController> logger,
            IPointInteretRepository repository,
            ICategorieRepository categorieRepo,
            ICommuneRepository communeRepo)

            : base(logger, repository)
        {
            _categorieRepo = categorieRepo;
            _communeRepo = communeRepo;
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