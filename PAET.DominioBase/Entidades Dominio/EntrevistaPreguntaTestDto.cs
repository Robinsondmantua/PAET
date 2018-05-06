using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.DominioBase.Entidades_Dominio
{
    public class EntrevistaPreguntaTestDto
    {
        public int IdEntrevistaPreguntaRespuesta { get; set; }
        public Nullable<int> IdEntrevistaPregunta { get; set; }
        public Nullable<int> IdPregunta { get; set; }
        public Nullable<int> IdRespuesta { get; set; }
    }
}
