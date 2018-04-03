using PAET.Comun;
using PAET.DominioBase.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PAET.ServiciosBasicos
{
    public static class ServiceUtils
    {
        public enum ModoAuditoria
        {
            Creacion,
            Modificacion
        }

        public static T AddInfoAuditoria<T>(T dtoAuditable, ModoAuditoria mode) where T : IAuditableEntity
        {
            ResultadoAccion rInfo = new ResultadoAccion();
            if (mode == ModoAuditoria.Creacion)
            {
                dtoAuditable.FechaCreacion = rInfo.FechaAccion;
                dtoAuditable.UsuarioCreacion = rInfo.Usuario;
            }
            if (mode == ModoAuditoria.Modificacion)
            {
                dtoAuditable.FechaModificacion = rInfo.FechaAccion;
                dtoAuditable.UsuarioModificacion = rInfo.Usuario;
            }
            return dtoAuditable;
        }



   
    }
}
