using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.DominioBase.Entidades_Dominio
{
    public class MultimediaDto
    {
        public int IdMultimedia { get; set; }
        public string Descripcion { get; set; }
        public virtual IEnumerable<MultimediaEntrevistaCandidatoDto> MultimediaEntrevistaCandidato { get; set; }
    }
}
