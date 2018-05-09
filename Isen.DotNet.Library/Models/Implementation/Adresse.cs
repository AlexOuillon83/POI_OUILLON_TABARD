using System;
using System.Collections.Generic;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class Adresse : BaseModel
    { 
        public string Nom { get; set;}
        public double ZipCode { get; set; }

        public Commune Commune { get; set; }

        public float Longitude {get; set; }

        public float Latitude {get; set; }

        public override dynamic ToDynamic()
        {
            var response = base.ToDynamic();
            response.nom = Nom;
            response.zipcode = ZipCode;
            response.latitude = Latitude;
            response.longitude = Longitude;
            return response;
        }
    }
}