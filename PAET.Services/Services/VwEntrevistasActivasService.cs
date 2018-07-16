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
    public class VwEntrevistasActivasService : ServiceBase<VwEntrevistasActivasDto, VwEntrevistasActivas>, IVwEntrevistasActivasService
    {
        protected TestEntities _context;

        public VwEntrevistasActivasService(TestEntities context) : base(context)
        {
            _context = context;
        }
    }
}
