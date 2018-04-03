using System;
using System.Collections.Generic;
using System.Web.Caching;

namespace PAET.ServiciosBasicos.Behaviours
{
    public interface ICache
    {
        object Recuperar(string clave);
        void Meter(string clave, object valor);
        void Meter(string clave, object valor, TimeSpan tiempo);
        void Meter(string clave, object valor, TimeSpan tiempo, CacheItemPriority prioridad);
        void MeterPermanente(string clave, object valor);
        bool Existe(string clave);
        void Borrar(string clave);
        void Limpiar();
        List<string> GetListaClaves();
    }
}
