
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.ServiciosBasicos.Behaviours
{

    public class CacheableAttribute : HandlerAttribute
    {
        public string CacheKey { get; set; }

        public CacheableAttribute() { }
        public CacheableAttribute(string cacheKey)
        {
            this.CacheKey = cacheKey;
        }

        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new CacheableHandler();
        }
    }

    internal class CacheableHandler : ICallHandler
    {
        public string CacheKey { get; set; }
        public CacheableHandler() { }

        public CacheableHandler(string cacheKey)
        {
            this.CacheKey = cacheKey;
        }

        public int Order { get; set; }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            IMethodReturn returnResult;
            ICache cache = new MiCache();

            //Ej: PAET.Namespace.MethodName&arg1value&arg1value&...&argnvalue
            if (this.CacheKey == null)
            {
                this.CacheKey = $"{input.MethodBase.DeclaringType.FullName}.{input.MethodBase.Name}";
                for (int i = 0; i < input.Arguments.Count; i++)
                    
                    this.CacheKey += (input.Arguments[i]!=null) ? $"&{input.Arguments[i].GetHashCode()}" : "&null";
            };

            Stopwatch sw = Stopwatch.StartNew();
            var objResultCache = cache.Recuperar(this.CacheKey);
            if (objResultCache == null) //no estaba en cache, Recuperamos de cache y lo añadimos.
            {
                returnResult = getNext()(input, getNext);
                cache.Meter(this.CacheKey, returnResult.ReturnValue, TimeSpan.FromSeconds(300));
            }
            else
            {
                returnResult = new VirtualMethodReturn(input, null);
                returnResult.ReturnValue = objResultCache;

            }
            sw.Stop();
            //FicheroLog.Debug($"[{((objResultCache != null) ? "CACHED" : "NOT_CACHED")}] {this.CacheKey} obtenido en {sw.ElapsedMilliseconds}ms");
            return returnResult;
        }
    }
}
