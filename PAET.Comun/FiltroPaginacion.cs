using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.Comun
{
    public class FiltroPaginacion : System.EventArgs
    {
        /// <summary>
        /// Filtro en  la Pagina 1 y 20 Items. 
        /// </summary>
        public static FiltroPaginacion Standar
        {
            get
            {
                return new FiltroPaginacion(0,20);
            }
        }

        /// <summary>
        /// Filtro sin limite de items por pagina.
        /// </summary>
        public static FiltroPaginacion SinLimite
        {
            get
            {
                return new FiltroPaginacion(0, 0);
            }
        }

        public int Pagina { get; set; }
        public int ItemsPorPagina { get; set; }

        public int ItemsParaSaltar
        {
            get
            {
                return ItemsPorPagina * Pagina;
            }
        }

        public FiltroPaginacion(int pagina, int itemsPorPagina)
        {
            this.ItemsPorPagina = itemsPorPagina;
            this.Pagina = pagina;
        }
        public FiltroPaginacion(int pagina) : this(pagina, 20) { }
        public FiltroPaginacion() : this(0, 20) { }
    }
}
