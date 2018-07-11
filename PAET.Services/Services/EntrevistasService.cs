using PAET.DominioBase.Entidades_Dominio;
using PAET.Infraestructure;
using PAET.Services.Interfaces;
using PAET.ServiciosBasicos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.Services.Services
{
    public class EntrevistasService : ServiceBase<EntrevistasDto, Entrevistas>, IEntrevistaService
    {
        protected TestEntities _context;
        public EntrevistasService(TestEntities context) : base(context)
        {
            _context = context;
        }
    }
}
