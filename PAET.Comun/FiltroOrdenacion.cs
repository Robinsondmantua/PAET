using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace PAET.Comun
{
    [Serializable]
    public class FiltroOrdenacion
    {
        public static FiltroOrdenacion SinOrdenacion
        {
            get
            {
                return new FiltroOrdenacion();
            }
        }

        public string PropiedadOrdenacion { get; set; }
        public int Direccion { get; set; }
        public string DireccionParaSqlQuery
        {
            get
            {
                if (Direccion == (int)SortDirection.Ascending)
                {
                    return "ASC";
                }
                else
                {
                    return "DESC";
                }
            }
        }

        public FiltroOrdenacion() : this(string.Empty, 0)
        { }

        public FiltroOrdenacion(string propiedadOrdenacion, int direccion)
        {
            PropiedadOrdenacion = propiedadOrdenacion;
            Direccion = direccion;
        }

    }
}
