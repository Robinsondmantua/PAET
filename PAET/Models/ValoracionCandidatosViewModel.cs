using PAET.DominioBase.Entidades_Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PAET.Models
{
    public class ValoracionCandidatosViewModel
    {
        public IEnumerable<EntrevistasDto> Entrevistas { get; set; }
        public IEnumerable<VwEntrevistaCandidatosDto> EntrevistaCandidato { get; set; }
    }
}