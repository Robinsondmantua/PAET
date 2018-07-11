using PAET.Comun;
using PAET.DominioBase.Entidades_Dominio;
using PAET.Infraestructure;
using PAET.Log.Log4Net;
using PAET.Services.Interfaces;
using PAET.ServiciosBasicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PAET.Services.Services
{
    public class CandidatosService : ServiceBase<CandidatosDto, Candidatos>, ICandidatosService
    {
        protected TestEntities _context;

        public CandidatosService(TestEntities context) : base(context)
        {
            _context = context;
        }
        public ResultadoAccion<CandidatosDto> ComprobarAccesoCorrecto(String usuario, String pwd)
        {
            ResultadoAccion<CandidatosDto> respuestaaccesocorrecto = new ResultadoAccion<CandidatosDto>();
            SHA512 shaMaplicado = new SHA512Managed();
            Byte[] _pwd = shaMaplicado.ComputeHash(Encoding.Unicode.GetBytes(pwd));
            try
            {
                CandidatosDto candidato = this.FindSingle(x => x.Apodo == usuario);
                if (candidato != null)
                {
                    if (Convert.ToBase64String(candidato.Pwd).Equals(Convert.ToBase64String(_pwd)))
                    {
                        if (!candidato.Activo)
                        {
                            respuestaaccesocorrecto = ResultadoAccion<CandidatosDto>.ResultadoError(ResultadoAccion.CodigoResultado.ERR, "No se ha podido verificar la identidad del candidato");
                            FicheroLog.Err("El candidato {0} no está activo.", usuario);
                        }
                        respuestaaccesocorrecto.Entidad = candidato;
                    }
                }
                else respuestaaccesocorrecto = ResultadoAccion<CandidatosDto>.ResultadoError(ResultadoAccion.CodigoResultado.ERR, "No se ha podido verificar la identidad del candidato");
            }
            catch (Exception ex)
            {
                respuestaaccesocorrecto = ResultadoAccion<CandidatosDto>.ResultadoError(ResultadoAccion.CodigoResultado.ERR, "No se ha podido verificar la identidad del candidato");
                FicheroLog.Err("Acceso incorrecto del candidato {0}", usuario);
            }
            return respuestaaccesocorrecto;
        }
    }
}
