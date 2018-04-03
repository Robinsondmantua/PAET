using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.DominioBase.Entidades_Dominio
{
    public class EntrevistasDto
    {
        public int IdEntrevista { get; set; }
        public string Descripcion { get; set; }
        public Nullable<System.DateTime> Fecha_Inicio { get; set; }
        public Nullable<System.DateTime> Fecha_Fin { get; set; }
        public IEnumerable<ChatEntrevistaCandidatoDto> ChatEntrevistaCandidato { get; set; }
        public IEnumerable<EntrevistaCandidatoDto> EntrevistaCandidato { get; set; }
        public IEnumerable<EntrevistaPreguntaDto> EntrevistaPregunta { get; set; }
    }
}
