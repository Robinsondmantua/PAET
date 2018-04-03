using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.DominioBase.Entidades_Dominio
{
    public class PreguntasDto
    {
        public int IdPregunta { get; set; }
        public Nullable<int> IdTecnologia { get; set; }
        public Nullable<int> IdDificultad { get; set; }
        public Nullable<int> IdValoracion { get; set; }
        public Nullable<int> IdTipoPregunta { get; set; }
        public string Descripcion { get; set; }
        public IEnumerable<RespuestasDto> Respuestas { get; set; }
    }
}
