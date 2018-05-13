using System;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class Categorie: BaseModel
    {
        public override string Nom { get; set; }
        public string Description { get; set;}

        public override dynamic ToDynamic()
        {
            var response = base.ToDynamic();
            response.nom = Nom;
            response.description = Description;
            return response;
        }
    }
}