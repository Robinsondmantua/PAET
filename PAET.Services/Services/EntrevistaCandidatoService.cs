using AutoMapper;
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
    public class EntrevistaCandidatoService : ServiceBase<EntrevistaCandidatoDto, EntrevistaCandidato>, IEntrevistaCandidatoService
    {
        protected TestEntities _context;

        public EntrevistaCandidatoService(TestEntities context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<VwEntrevistaCandidatosDto> ObtenerCandidatosEntrevistaNombre(int idEntrevista)
        {
            return Mapper.Map<IEnumerable<VwEntrevistaCandidatosDto>>(_context.VwEntrevistaCandidatos.Where(x => x.IdEntrevista == idEntrevista));
        }
    }
}
