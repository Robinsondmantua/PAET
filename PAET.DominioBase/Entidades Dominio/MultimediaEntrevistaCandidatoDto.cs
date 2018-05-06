using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.DominioBase.Entidades_Dominio
{
    public class MultimediaEntrevistaCandidatoDto
    {
        public int IdMultiEntrevistaCandidato { get; set; }
        public Nullable<int> IdEntrevistaCandidato { get; set; }
        public Nullable<int> IdValoracion { get; set; }
        public byte[] Contenido { get; set; }
        public bool Finalizacion { get; set; }
        public string UsuarioValoracion { get; set; }
        public Nullable<System.DateTime> FechaValoracion { get; set; }
        public Nullable<int> IdMultimedia { get; set; }
    }
}
