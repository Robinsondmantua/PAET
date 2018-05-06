using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.DominioBase.Entidades_Dominio
{
    public class EntrevistaPreguntaDto
    {
        public int IdEntrevistaPregunta { get; set; }
        public Nullable<int> IdEntrevistaCandidato { get; set; }
        public Nullable<int> IdValoracion { get; set; }
        public Nullable<System.DateTime> Fecha_Inicio { get; set; }
        public bool Finalizacion { get; set; }
        public int NumPreguntas { get; set; }
        public IEnumerable<EntrevistaPreguntaTestDto> EntrevistaPreguntaTest { get; set; }
    }
}
