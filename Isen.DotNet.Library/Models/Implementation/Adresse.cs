using System;
using System.Collections.Generic;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class Adresse : BaseModel
    { 

        public string Texte { get; set;}
        public double ZipCode { get; set; }

        public Commune commune { get; set;}
        public List<Person> PersonCollection { get;set; }
        public int? PersonCount => PersonCollection?.Count;

        public override dynamic ToDynamic()
        {
            var response = base.ToDynamic();
            response.nb = PersonCount;
            return response;
        }
    }
}