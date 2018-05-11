using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Threading.Tasks;
using Isen.DotNet.Library.Models.Implementation;

namespace ViewComponentSample.ViewComponents
{
    public class AdresseFormViewComponent : ViewComponent
    {

        public AdresseFormViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(
        Adresse adresse, IEnumerable<Commune> communes, IEnumerable<Departement> departements)
        {
            ViewBag.Communes = communes;
            ViewBag.Departements = departements;
            return View(adresse);
        }
    }
}