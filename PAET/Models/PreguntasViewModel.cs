using PAET.DominioBase.Entidades_Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PAET.Models
{
    public class PreguntasViewModel: PreguntasDto
    {
        public IEnumerable<GeneralDto> Tecnologias { get; set; }
        public IEnumerable<GeneralDto> Dificultad { get; set; }
        public IEnumerable<GeneralDto> Valoracion { get; set; }
        public IEnumerable<GeneralDto> TipoPregunta { get; set; }
    }
}