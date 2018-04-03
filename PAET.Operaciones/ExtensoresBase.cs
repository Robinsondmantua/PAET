using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.Operaciones
{
    public static class ExtensoresBase
    {
        static Random aleatorio = new Random();

        public static List<T> SeleccionarElementoAleatorio<T> (this List<T> lista, int numeromaximo)
        {
            int seleccionado;
            List<T> resultado = new List<T>();

            for (int i=0;i<numeromaximo;i++)
            {
                seleccionado = aleatorio.Next(0,lista.Count());
                resultado.Add(lista[seleccionado]);
                lista.RemoveAt(seleccionado);
                if (lista.Count == 0) return resultado;
            }

            return resultado;
        }
    }
}
