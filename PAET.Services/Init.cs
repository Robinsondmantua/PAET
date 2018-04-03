using AutoMapper;
using CommonServiceLocator;
using PAET.Services.Interfaces;
using PAET.Services.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.Interceptors.InstanceInterceptors.InterfaceInterception;
using Unity.ServiceLocation;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(PAET.Services.Init), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(PAET.Services.Init), "Shutdown")]
namespace PAET.Services
{
    public class Init
    {
        public static void Start()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<PAETProfile>();
            });

            GetConfiguredContainer();
        }
        public static void Shutdown()
        {
            var container = GetConfiguredContainer();
            container.Dispose();
        }

        #region Unity Container

        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            container.AddNewExtension<Interception>();

            RegisterTypes(container);
            SetServiceLocator(container);

            return container;
        });

        /// <summary>
        /// Gets the configured Unity Container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        /// <summary>
        /// Implementación de ServiceLocator. 
        /// Prescindir en la Migración a MVC ya que se considera Bad Practice
        /// </summary>
        /// <param name="container"></param>
        public static void SetServiceLocator(IUnityContainer container)
        {
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));
        }

        /// <summary>Registers the type mappings with the Unity Container.</summary>
        /// <param name="container">The unity Container to configure.</param>        
        public static void RegisterTypes(IUnityContainer container)
        {
            //TODO: Register Type                        
            container.RegisterType<IPreguntasService, PreguntasService>()
                .Configure<Interception>()
                .SetInterceptorFor<IPreguntasService>(new InterfaceInterceptor());

        }

        #endregion        
    }
}

