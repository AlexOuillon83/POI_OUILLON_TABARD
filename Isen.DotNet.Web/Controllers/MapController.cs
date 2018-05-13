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
    public class MapController :  BaseController<PointInteret>{
        private readonly IPointInteretRepository _pointinteretRepo; 
        public MapController(
            ILogger<PointInteretController> logger,
            IPointInteretRepository repository
        ) : base(logger, repository)
        {
            _pointinteretRepo = repository;    
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
    }
}