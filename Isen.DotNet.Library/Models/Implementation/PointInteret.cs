using System;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation {
    public class PointInteret : BaseModel {

        public string Nom { get; set; }
        public Adresse Adresse { get; set; }
        public Categorie Categorie { get; set; }
        public string _nom;
        public string _descriptif;
        public Categorie  _categorie;

        public Commune Commune { get; set; }

        public override string Name {
            get { return Nom; }
            set { Nom = value; }
        }

        public string Descriptif {
            get { return _descriptif; }
            set { _descriptif = value; }
        }

        public override string ToString(){
            return String.Format("Texte: {0}, {1} {2} {3}", this.Nom, this.Descriptif, this.Categorie?.Nom, this.Adresse?.Texte);
        }

      
        public override dynamic ToDynamic () {
            var response = base.ToDynamic ();
            response.nom = Nom;
            response.categorie = Categorie;
            response.descriptif = Descriptif;
            response.adresse = Adresse;
            return response;
        }
    }
}