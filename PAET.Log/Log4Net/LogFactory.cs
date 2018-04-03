using System;
using System.IO;
using System.Security.Permissions;

namespace PAET.Log.Log4Net
{
    public static class LogFactory
    {
        /// <summary>
        /// Crea una instancia del Logger Log4Net por Clase.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Configura el repositorio si aun no ha sido configurado.
        /// </para>
        /// </remarks>
        /// <returns>Instancia del Logger.</returns>
        public static log4net.ILog Create()
        {            
            var log = log4net.LogManager.GetLogger("");

            if (log4net.LogManager.GetRepository().Configured == false)
            {
                try
                {
                    var fiop = new FileIOPermission(FileIOPermissionAccess.Read, AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                    fiop.Demand();

                    var configFile = new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                    log4net.Config.XmlConfigurator.ConfigureAndWatch(configFile);
                }
                catch (System.Security.SecurityException e)
                {
                    log.DebugFormat("No se puedo recuperar el fichero de configuración debido a un problema de permisos. {0}", e);
                }
            }

            log.DebugFormat("Logging {0}", log.Logger.Name);

            return log;
        }
    }
}
