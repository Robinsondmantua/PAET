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
        public Nullable<int> IdEntrevista { get; set; }
        public Nullable<int> IdPregunta { get; set; }
        public Nullable<int> IdValoracion { get; set; }

        public EntrevistasDto Entrevistas { get; set; }
        public PreguntasDto Preguntas { get; set; }
        public ValoracionDto Valoracion { get; set; }
    }
}
