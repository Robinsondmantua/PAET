using EMVS.Comun.DominioBase.PAET;
using EMVS.Comun.ServiciosBasicos;
using EMVS.Comun.Negocio; 
using PAET.Infraestructure;
using PAET.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.Services.Services
{
    public class PreguntasService: ServiceBase<PreguntasDto, Preguntas>, IPreguntasService
    {
        protected TestEntities _context;

        public PreguntasService(TestEntities context) : base(context)
        {
            _context = context;
        }

        public List<PreguntasDto> GenerarTestRandom(IEnumerable<int> tecnologias)
        {
            List<PreguntasDto> preguntasxtecnologia = this.Find(x=> tecnologias.Contains(x.IdTecnologia.Value)).ToList();

            List<PreguntasDto> lstpreguntasrandom = preguntasxtecnologia.SeleccionarElementoAleatorio(5);

            return lstpreguntasrandom;             
        }
    }
}
