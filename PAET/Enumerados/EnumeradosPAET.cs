using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PAET.Enumerados
{
    public class EnumeradosPAET
    {
        public enum TipoPregunta : int { Opciones = 1, Codigo = 2, Libre = 3 , MixtaCodigo = 4};
        public enum TipoTecnologia { javascript = 1, vbnet = 2, csharp = 3, sql = 4 };
    }
}