using System;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class Categorie: BaseModel
    {
        public string nomCategorie { get; set; }
        public string descriptifCategorie { get; set;}

        private string _nomCategorie;
        private string _descriptifCategorie;

        public override string Name
        {
            get { return _nomCategorie; }
            set { _nomCategorie = value; }
        }

         public override string Descriptif
        {
            get { return _descriptifCategorie; }
            set { _descriptifCategorie = value; }
        }

        public override dynamic ToDynamic()
        {
            var response = base.ToDynamic();
            response.nom = nomCategorie;
            response.descriptifCategorie = descriptifCategorie;
            return response;
        }
    }
}