using System;
using System.Collections.Generic;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class Commune : BaseModel
    {
        public String Nom { get; set; }
        public float Longitude { get; set; }
        public float Latitude {get; set; }


        public override dynamic ToDynamic()
        {
            var response = base.ToDynamic();
            response.nom = Nom;
            response.longitude = Longitude;
            response.latitude = Latitude;
            return response;
        }
    }
}