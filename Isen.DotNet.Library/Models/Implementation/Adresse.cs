using System;
using System.Collections.Generic;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class Adresse : BaseModel
    { 

        public string Texte { get; set;}
        public string ZipCode { get; set; }

        public Commune Commune { get; set; }

        public float Longitude {get; set; }

        public float Latitude {get; set; }


        //Variable Texte ne retourne rien 
        public override string ToString(){
            return String.Format("Texte: {0}, {1} {2} - ({3},{4})", Texte, ZipCode, Commune?.Nom, Longitude, Latitude);
        }

        public override dynamic ToDynamic()
        {
            var response = base.ToDynamic();
            response.texte = Texte;
            response.name = Name;
            response.zipcode = ZipCode;
            response.latitude = Latitude;
            response.longitude = Longitude;
            return response;
        }
    }
}