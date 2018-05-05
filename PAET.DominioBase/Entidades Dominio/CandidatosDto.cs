using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.DominioBase.Entidades_Dominio
{
    public class CandidatosDto
    {
        public int IdCandidato { get; set; }
        public Nullable<int> IdCategoria { get; set; }
        public Nullable<int> IdTitulacion { get; set; }
        public Nullable<int> IdExperiencia { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string NIFNIE { get; set; }
        public string Apodo { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public byte[] Pwd { get; set; }
        public bool Activo { get; set; }
        public IEnumerable<ChatEntrevistaCandidatoDto> ChatEntrevistaCandidato { get; set; }
        public IEnumerable<EntrevistaCandidatoDto> EntrevistaCandidato { get; set; }
    }
}
