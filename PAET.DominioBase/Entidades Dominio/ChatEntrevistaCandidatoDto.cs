using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.DominioBase.Entidades_Dominio
{
    public class ChatEntrevistaCandidatoDto
    {
        public int IdChat { get; set; }
        public int IdEntrevista { get; set; }
        public int IdCandidato { get; set; }
        public Nullable<int> IdValoracion { get; set; }

    }
}
