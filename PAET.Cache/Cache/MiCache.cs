using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;

namespace PAET.ServiciosBasicos.Behaviours
{
    public class MiCache : ICache
    {
        private static System.Web.Caching.Cache _cache = HttpContext.Current.Cache;

        private static readonly TimeSpan TimeStam = new TimeSpan(0, 0, 50, 0);

        public MiCache()
        {
            _cache = HttpContext.Current.Cache;
        }

        public object Recuperar(string clave)
        {
            return _cache.Get(clave);
        }

        public void Meter(string clave, object valor)
        {
            Meter(clave, valor, TimeStam);
        }

        public void Meter(string clave, object valor, TimeSpan tiempo)
        {
            Meter(clave, valor, tiempo, CacheItemPriority.Normal);
        }

        public void Meter(string clave, object valor, TimeSpan tiempo, CacheItemPriority prioridad)
        {
            _cache.Insert(clave, valor, null, DateTime.MaxValue, tiempo, prioridad, null);
        }

        public void MeterPermanente(string clave, object valor)
        {
            _cache.Insert(clave, valor, null, DateTime.MaxValue, System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }

        public bool Existe(string clave)
        {
            return _cache[clave] != null;
        }

        public void Borrar(string clave)
        {
            if (Existe(clave))
            {
                _cache.Remove(clave);
            }
        }

        public void Limpiar()
        {
            foreach (var clave in GetListaClaves())
            {
                Borrar(clave);
            }
        }

        public List<string> GetListaClaves()
        {
            var claves = new List<string>();
            var lector = _cache.GetEnumerator();
            while (lector.MoveNext())
            {
                claves.Add(lector.Key.ToString());
            }

            return claves;
        }
    }
}
