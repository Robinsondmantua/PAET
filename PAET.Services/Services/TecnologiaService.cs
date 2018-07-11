using PAET.DominioBase.Entidades_Dominio;
using PAET.Infraestructure;
using PAET.Services.Interfaces;
using PAET.ServiciosBasicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.Services.Services
{
    public class TecnologiaService: ServiceBase<TecnologiaDto, Tecnologia>, ITecnologiaService
    {
        protected TestEntities _context;

        public TecnologiaService(TestEntities context) : base(context)
        {
            _context = context;
        }

    }
}
