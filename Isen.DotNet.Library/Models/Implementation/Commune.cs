using System;
using System.Collections.Generic;
using Isen.DotNet.Library.Models.Base;

namespace Isen.DotNet.Library.Models.Implementation
{
    public class Commune : BaseModel
    { 

        public override dynamic ToDynamic()
        {
            var response = base.ToDynamic();
            
            return response;
        }
    }
}