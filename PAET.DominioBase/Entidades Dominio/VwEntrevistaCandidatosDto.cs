using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.DominioBase.Entidades_Dominio
{
    public class VwEntrevistaCandidatosDto
    {
        public int IdCandidato { get; set; }
        public int IdEntrevistaCandidato { get; set; }
        public Nullable<int> IdEntrevista { get; set; }
        public string NombreCompleto { get; set; }
        public Nullable<decimal> ValoracionFinal { get; set; }
    }
}
