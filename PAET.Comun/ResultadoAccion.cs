using PAET.Comun;
using System;

namespace PAET.Comun {
    public class ResultadoAccion {
        public enum CodigoResultado {
            OK = 0,
            ERR = 1,
            NOT_FOUND = 2,
            WARNING = 3
        }



        public string _mensajeExito;            //buscar solucción para traspasar el mensaje entre resultados...
        public string _mensajeError = "";       //buscar solucción para traspasar el mensaje entre resultados...

        public Exception ResultException { get; set; }
        public CodigoResultado ResultCode { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaAccion { get; set; }

        public string ResultMsg {
            get {
                return this.Ok() ? _mensajeExito : _mensajeError;
            }
        }

        public ResultadoAccion() {
            if (System.Web.HttpContext.Current != null) {
                if (System.Web.HttpContext.Current.Request.LogonUserIdentity != null) {
                    Usuario = System.Web.HttpContext.Current.Request.LogonUserIdentity.Name;
                }
                if (System.Web.HttpContext.Current.Timestamp != null) {
                    FechaAccion = System.Web.HttpContext.Current.Timestamp;
                } else {
                    FechaAccion = DateTime.Now;
                }

            }


            ResultCode = CodigoResultado.OK;
            _mensajeExito = "Operacion realizada correctamente.";
        }

        #region "Metodos Estaticos - Respuestas Genericas"

        public static ResultadoAccion ResultadoOK() {
            var resAccion = new ResultadoAccion { ResultCode = CodigoResultado.OK };
            return resAccion;
        }

        public static ResultadoAccion ResultadoOK(string msg) {
            var resAccion = new ResultadoAccion { ResultCode = CodigoResultado.OK };
            resAccion.AddMensajeOk(msg);
            return resAccion;
        }

        public static ResultadoAccion ResultadoError(CodigoResultado codigoResultado,string msg) {
            if (codigoResultado == ResultadoAccion.CodigoResultado.OK) {
                throw new Exception("No se puede crear un objeto de Error con codigo de resultado 'Ok'.");
            }

            var resAccion = new ResultadoAccion { ResultCode = codigoResultado };
            resAccion.AddMensajeError(msg);
            return resAccion;
        }

        public static ResultadoAccion ResultadoError(CodigoResultado codigoResultado) {
            return ResultadoError(codigoResultado,"Ha ocurrido un error.");
        }

        public static ResultadoAccion ResultadoError(string errorMessage) {
            return ResultadoError(CodigoResultado.ERR,errorMessage);
        }

        #endregion

        /// <summary>
        /// Indica si el proceso ha ido correctamente
        /// </summary>
        /// <remarks></remarks>
        public bool Ok() {
            return ResultCode == CodigoResultado.OK;
        }

        public bool Warning() {
            return ResultCode == CodigoResultado.WARNING;
        }

        public void AddMensajeOk(string msg) {
            _mensajeExito = msg;
        }

        public void AddMensajeError(string msg) {
            _mensajeError += msg + Environment.NewLine;
        }

        public void AñadirMensaje(string mensaje) {
            AddMensajeOk(mensaje);
            AddMensajeError(mensaje);
        }

        public void LimpiarMensajes() {
            _mensajeExito = string.Empty;
            _mensajeError = string.Empty;
        }

        public override string ToString() {
            return $"ResultCode: {ResultCode}, ResultMsg: {ResultMsg}";
        }
    }
}

namespace PAET.Comun
{
    public class ResultadoAccion<T> : ResultadoAccion {
        public T Entidad { get; set; }

        public static ResultadoAccion<T> CreateFromResultado(ResultadoAccion resultado) {
            ResultadoAccion<T> resAccion = new ResultadoAccion<T> {
                ResultCode = resultado.ResultCode,
                FechaAccion = resultado.FechaAccion,
                Usuario = resultado.Usuario,
                _mensajeError = resultado._mensajeError,
                _mensajeExito = resultado._mensajeExito,
                ResultException = resultado.ResultException
            };
            return resAccion;
        }
        public static ResultadoAccion<T> CreateFromResultado(ResultadoAccion resultado,T entidad) {
            ResultadoAccion<T> resAccion = new ResultadoAccion<T> {
                ResultCode = resultado.ResultCode,
                FechaAccion = resultado.FechaAccion,
                Usuario = resultado.Usuario,
                _mensajeError = resultado._mensajeError,
                _mensajeExito = resultado._mensajeExito,
                ResultException = resultado.ResultException,
                Entidad = entidad
            };
            return resAccion;
        }

        public static ResultadoAccion<T> ResultadoOK(T entidad) {
            ResultadoAccion<T> resAccion = new ResultadoAccion<T> {
                ResultCode = CodigoResultado.OK,
                Entidad = entidad
            };
            return resAccion;
        }


        //TODO REFACTOR. Añadido por compatibilidad para dar soporte a los mismos metodos cuando hay entidades.
        #region "Metodos Estaticos - Respuestas Genericas con entidades."

        public static new ResultadoAccion<T> ResultadoOK() {
            var resAccion = new ResultadoAccion<T> { ResultCode = CodigoResultado.OK };
            return resAccion;
        }

        public static new ResultadoAccion<T> ResultadoOK(string msg) {
            var resAccion = new ResultadoAccion<T> { ResultCode = CodigoResultado.OK };
            resAccion.AddMensajeOk(msg);
            return resAccion;
        }

        public static new ResultadoAccion<T> ResultadoError(CodigoResultado codigoResultado,string msg) {
            if (codigoResultado == CodigoResultado.OK) {
                throw new Exception("No se puede crear un objeto de Error con codigo de resultado 'Ok'.");
            }

            var resAccion = new ResultadoAccion<T> { ResultCode = codigoResultado };
            resAccion.AddMensajeError(msg);
            return resAccion;
        }

        public static new ResultadoAccion<T> ResultadoError(CodigoResultado codigoResultado) {
            return ResultadoError(codigoResultado,"Ha ocurrido un error.");
        }

        public static new ResultadoAccion<T> ResultadoError(string errorMessage) {
            return ResultadoError(CodigoResultado.ERR,errorMessage);
        }

        #endregion
    }
}

