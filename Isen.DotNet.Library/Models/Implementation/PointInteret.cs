using System;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class PointInteret : BaseModel
    {
        
        public string Nom { get; set; }
        public Adresse _Adresse { get; set; }
        public Categorie categorie { get; set; }
        //public City City { get;set; }
        //public int? CityId { get;set; }

        private string _nom;
        private string _descriptif;

        private Categorie _categorie;

        private Adresse _adresse; 
        
        public override string Name 
        {
            get { return _nom ?? $" {Nom}"; }
            set { _nom = value; }
        }

        public override string Descriptif
        {
            get { return _descriptif; }
            set { _descriptif = value; }
        }
        
        //public override string Display =>
            //$"{base.Display}|Age={Age}|City={City}"; 

        public override dynamic ToDynamic()
        {
            var response = base.ToDynamic();
            response.nom = Nom;
            //response.age = Age;
            //response.city = City?.ToDynamic();
            return response;
        }
    }
}