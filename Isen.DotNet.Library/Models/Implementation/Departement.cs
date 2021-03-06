using System;
using System.Collections.Generic;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class Departement : BaseModel
    {
        public String Nom { get; set; }
        public List<Commune> CommuneCollection { get; set; }

        public override dynamic ToDynamic()
        {
            var response = base.ToDynamic();
            response.nom = Nom;
            return response;
        }
    }
}