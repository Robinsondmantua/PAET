using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Unity.AspNet.Mvc;
using EMVS.Comun.ServiciosBasicos.Mapping;
using PAET.Services.Profiles;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(PAET.UnityMvcActivator), nameof(PAET.UnityMvcActivator.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(PAET.UnityMvcActivator), nameof(PAET.UnityMvcActivator.Shutdown))]

namespace PAET
{
    /// <summary>
    /// Provides the bootstrapping for integrating Unity with ASP.NET MVC.
    /// </summary>
    public static class UnityMvcActivator
    {
        /// <summary>
        /// Integrates Unity when the application starts.
        /// </summary>
        public static void Start() 
        {
            Mapper.Initialize(cfg =>
            {
                cfg.ConfigureDefaultDtoMapping();  // Mapeo EntityDTO -> Entity || Entity -> EntityDTO por defecto.
                cfg.AddProfile<PAETProfile>();
            });

            //var container = UnityConfig.GetConfiguredContainer();
            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(UnityConfig.Container));

            DependencyResolver.SetResolver(new UnityDependencyResolver(UnityConfig.Container));

            // TODO: Uncomment if you want to use PerRequestLifetimeManager
            // Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }

        /// <summary>
        /// Disposes the Unity container when the application is shut down.
        /// </summary>
        public static void Shutdown()
        {
            var container = UnityConfig.Container;
            UnityConfig.Container.Dispose();
        }
    }
}