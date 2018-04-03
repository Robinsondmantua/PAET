using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.DominioBase.Entidades_Dominio
{
    public class EntrevistaCandidatoDto
    {
        public int IdEntrevistaCandidato { get; set; }
        public Nullable<int> IdEntrevista { get; set; }
        public Nullable<int> IdCandidato { get; set; }
        public Nullable<decimal> ValoracionFinal { get; set; }

        public CandidatosDto Candidatos { get; set; }
        public EntrevistasDto Entrevistas { get; set; }
    }
}
