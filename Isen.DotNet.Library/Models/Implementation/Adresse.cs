using System;
using System.Collections.Generic;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class Adresse : BaseModel
    { 
<<<<<<< HEAD
        public string Nom { get; set;}
        public double ZipCode { get; set; }
=======

        public string Texte { get; set;}
        public string ZipCode { get; set; }
>>>>>>> ece2764a4c1c2c4ea454622df992553bc8347c1a

        public Commune Commune { get; set; }

        public float Longitude {get; set; }

        public float Latitude {get; set; }

        public override string ToString(){
            return String.Format("Texte: {0}, {1} {2} - ({3},{4})", Texte, ZipCode, Commune?.Nom, Longitude, Latitude);
        }

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