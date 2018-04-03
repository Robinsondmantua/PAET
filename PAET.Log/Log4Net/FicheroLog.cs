using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;
using System.Xml.Serialization;
using log4net;
using log4net.Core;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using PAET.Comun;

namespace PAET.Log.Log4Net
{
    public class FicheroLog
    {
        private static readonly ILog Log = Create();

        public static void Debug(string msg, params object[] args)
        {
            Log.DebugFormat(LogMessage(msg), args);
        }
        public static void Err(string msg, params object[] args)
        {
            Log.ErrorFormat(LogMessage(msg), args);
        }
        public static void Err(string msg, Exception ex)
        {
            if (ex.InnerException != null)
            {
                Err("InnerException: ", ex.InnerException);
            }
            else
            {
                Log.Error(LogMessage(msg), ex);
            }

            if (ex.GetType() == typeof(System.Data.Entity.Validation.DbEntityValidationException))
            {
                var validationEx = ((System.Data.Entity.Validation.DbEntityValidationException)ex);
                foreach (var entityValidationError in validationEx.EntityValidationErrors)
                {
                    FicheroLog.Log.Warn($"Error al validar la entidad de tipo: {entityValidationError.Entry.Entity.GetType()}");
                    foreach (var validationError in entityValidationError.ValidationErrors)
                        Log.Warn(LogMessage($"[ERROR VALIDANDO LA ENTIDAD] - {validationError.PropertyName} : {validationError.ErrorMessage}"));
                }
            }
        }
        public static void Info(string msg, params object[] args)
        {
            Log.InfoFormat(LogMessage(msg), args);
        }
        public static void Info(string msg, SqlCommand cmd)
        {
            try
            {
                DbType[] quotedParameterTypes = new DbType[] {
                    DbType.AnsiString, DbType.Date,
                    DbType.DateTime, DbType.Guid, DbType.String,
                    DbType.AnsiStringFixedLength, DbType.StringFixedLength,
                    DbType.Boolean,DbType.Decimal, DbType.Int32, DbType.Int16, DbType.Int64
            };
                string query = msg;

                var arrParams = new SqlParameter[cmd.Parameters.Count];
                cmd.Parameters.CopyTo(arrParams, 0);

                foreach (SqlParameter p in arrParams)
                {
                    string value = p.Value.ToString();
                    if (quotedParameterTypes.Contains(p.DbType))
                        value = " '" + value + "',";
                    query += value;
                }
                query = query.Remove(query.Length - 1, 1) + "";
                Log.Info(LogMessage(query));

            }
            catch (Exception)
            {
                Log.Debug("Error al recuperar los datos de la consulta en el sqlcommand.");
            }
        }
        public static void Warn(string msg, params object[] args)
        {
            Log.WarnFormat(LogMessage(msg), args);
        }

        public static void LogResult(ResultadoAccion resultado)
        {
            switch (resultado.ResultCode)
            {
                case ResultadoAccion.CodigoResultado.OK:
                    Log.Debug(resultado);
                    break;
                case ResultadoAccion.CodigoResultado.ERR:
                    Log.Warn(resultado);
                    if (resultado.ResultException != null)
                    {
                        Err("Se devolvio una excepción con el resultado: ", resultado.ResultException);
                    }
                    break;
                //ponemos Warning para no confudir con excepciones no controladas.
                case ResultadoAccion.CodigoResultado.NOT_FOUND:
                    break;
                case ResultadoAccion.CodigoResultado.WARNING:
                    break;
                default:
                    Log.Warn(resultado);
                    break;
            }
        }


        public static void LogObj(object obj)
        {
            var xmlSerializer = new XmlSerializer(obj.GetType());
            try
            {
                using (var textWriter = new StringWriter())
                {
                    xmlSerializer.Serialize(textWriter, obj);
                    Log.Debug(textWriter.ToString());
                }
            }
            catch (Exception)
            {
                Log.Debug("Error al serializar el objeto.");
            }
        }

        public static void LogObj<T>(T obj)
        {
            var xmlSerializer = new XmlSerializer(obj.GetType());
            try
            {
                using (var textWriter = new StringWriter())
                {
                    xmlSerializer.Serialize(textWriter, obj);
                    Log.Debug(textWriter.ToString());
                }
            }
            catch (Exception)
            {
                Log.Debug("Error al serializar el objeto.");
            }
        }

        private static ILog Create()
        {
            //  Dim method = New StackTrace().GetFrame(3).GetMethod()
            var log = LogManager.GetLogger("");
            if (LogManager.GetRepository().Configured) return log;
            try
            {
                var fiop = new FileIOPermission(FileIOPermissionAccess.Read, AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                fiop.Demand();

                var configFile = new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                log4net.Config.XmlConfigurator.ConfigureAndWatch(configFile);
            }
            catch (System.Security.SecurityException e)
            {
                log.WarnFormat("No se puedo recuperar el fichero de configuración debido a un problema de permisos. {0}", e.Message);
            }
            return log;
        }

        private static string LogMessage(string msg)
        {
            try
            {
                var st = new StackTrace(2, true);
                var frame = st.GetFrame(0);
                var method = frame.GetMethod();

                var className = string.Empty;
                var methodName = string.Empty;

                if (method != null)
                {
                    methodName = method.Name;
                    if (method.DeclaringType != null)
                    {
                        className = method.DeclaringType.Name;
                    }
                }

                //0 -> Sin limite de caracteres por mensaje.                
                if (Convert.ToInt32(ConfigurationManager.AppSettings["MaxRowLength"]) == 0)
                    return $"[{className}.{methodName}] {msg}";
                if (msg.Length >= Convert.ToInt32(ConfigurationManager.AppSettings["MaxRowLength"]))
                {
                    msg = msg.Substring(0, Convert.ToInt32(ConfigurationManager.AppSettings["MaxRowLength"])) + "...";
                }

                return $"[{className}.{methodName}] {msg}";
            }
            catch (Exception)
            {
                return $"[not_defined] {msg}";
            }
        }
    }
}
