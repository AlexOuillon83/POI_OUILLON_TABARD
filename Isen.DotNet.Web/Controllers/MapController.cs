using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Isen.DotNet.Web.Models;
using Isen.DotNet.Library.Repositories.Interfaces;
using Isen.DotNet.Library.Repositories.InMemory;
using Isen.DotNet.Library.Models.Implementation;
using Microsoft.Extensions.Logging;

namespace Isen.DotNet.Web.Controllers
{
    public class MapController : Controller{
        private readonly IPointInteretRepository _pointinteretRepo; 
        public MapController(
            ILogger<PointInteretController> logger,
            IPointInteretRepository repository
        )
        {
            _pointinteretRepo = repository;    
        }

        public IActionResult Index(){            
            ViewData["Message"] = "Your application description page.";
            ViewBag.PointInterets = _pointinteretRepo.GetAll();
            return View();
        }
    }
}