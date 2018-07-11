using PAET.DominioBase.Entidades_Dominio;
using PAET.ServiciosBasicos.CoreService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.Services.Interfaces
{
    public interface IEntrevistaCandidatoService : IServiceBase<EntrevistaCandidatoDto>
    {
        IEnumerable<VwEntrevistaCandidatosDto> ObtenerCandidatosEntrevistaNombre(int idEntrevista);
    }
}
