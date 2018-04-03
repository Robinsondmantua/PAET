using System;
using System.Collections.Generic;
using System.Linq;

namespace PAET.Comun
{

    public class ResultadoPaginacion<T>
    {
        private IEnumerable<T> _resultados;
        private int _numeroPagina;
        private int _itemsPorPagina;
        private int _totalItems;
        private int _totalPaginas;

        public IEnumerable<T> Resultados
        {
            get
            {
                return _resultados;
            }
        }

        public int NumeroItemsPaginados
        {
            get
            {
                return _resultados.Count(); //TODO calcularlo mejor.
            }
        }
        public int NumeroItemsTotales
        {
            get
            {
                return _totalItems;
            }
        }

        public int NumeroPagina
        {
            get
            {
                return _numeroPagina;
            }
        }

        public int TotalPaginas
        {
            get
            {
                return (_totalItems > 0) ? (int) Math.Ceiling( (decimal) _totalItems / _itemsPorPagina) : 0;
            }
        }
        public ResultadoPaginacion(IEnumerable<T> items, int numeroPagina, int tamañoPagina,int totalItems)
        {
            _resultados = items;
            _numeroPagina = numeroPagina;
            _itemsPorPagina = tamañoPagina;
            _totalItems = totalItems;
        }
    
    }
}