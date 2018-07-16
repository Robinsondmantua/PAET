using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.DominioBase.Entidades_Dominio
{
    public class VwEntrevistasActivasDto
    {
        public Nullable<int> IdCandidato { get; set; }
        public Nullable<int> IdEntrevista { get; set; }
        public int IdEntrevistaCandidato { get; set; }
        public string EntrevistaTest { get; set; }
        public string Chat { get; set; }
        public string Multimedia { get; set; }
    }
}
