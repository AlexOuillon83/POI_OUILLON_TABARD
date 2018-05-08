using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Isen.DotNet.Library.Models.Implementation;

namespace FormsTagHelper.ViewModels
{
    public class CommuneViewModel
    {
        public Commune Commune { get; set; }

        public static List<SelectListItem> Communes()
        {
            List<SelectListItem> liste = new List<SelectListItem>();
            liste.Add(new SelectListItem { Value = "MX", Text = "Mexico" });
            liste.Add(new SelectListItem { Value = "CA", Text = "Canada" });
            liste.Add(new SelectListItem { Value = "US", Text = "USA"  });
            return liste;
        }
    }
}