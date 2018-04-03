using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.DominioBase.Entidades_Dominio
{
    public class RespuestasDto
    {
        public int IdRespuesta { get; set; }
        public Nullable<int> IdPregunta { get; set; }
        public Nullable<int> IdValoracion { get; set; }
        public string Descripcion { get; set; }
        public Nullable<bool> Correcta { get; set; }
    }
}
