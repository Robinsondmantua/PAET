using EMVS.Comun.CoreService.Contracts;
using EMVS.Comun.DominioBase.PAET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.Services.Interfaces
{
    public interface IPreguntasService : IServiceBase<PreguntasDto>
    {
        List<PreguntasDto> GenerarTestRandom(IEnumerable<int> tecnologias);
    }
}
