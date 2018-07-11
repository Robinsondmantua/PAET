using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.DominioBase.Entidades_Dominio
{
    public class PreguntasDto
    {
        [DisplayName("Identificador")]
        public int IdPregunta { get; set; }
        [DisplayName("Tecnología")]
        public Nullable<int> IdTecnologia { get; set; }
        [DisplayName("Dificultad")]
        public Nullable<int> IdDificultad { get; set; }
        [DisplayName("Valoración")]
        public Nullable<int> IdValoracion { get; set; }
        [DisplayName("Tipo Pregunta")]
        public Nullable<int> IdTipoPregunta { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        public IEnumerable<RespuestasDto> Respuestas { get; set; }
    }
}
