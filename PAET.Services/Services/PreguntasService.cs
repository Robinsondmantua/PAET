using PAET.Infraestructure;
using PAET.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAET.DominioBase.Entidades_Dominio;
using PAET.ServiciosBasicos;
using PAET.Operaciones;

namespace PAET.Services
{
    public class PreguntasService: ServiceBase<PreguntasDto, Preguntas>, IPreguntasService
    {
        protected TestEntities _context;

        public PreguntasService(TestEntities context) : base(context)
        {
            _context = context;
        }

        public List<PreguntasDto> GenerarTestRandom(IEnumerable<int> tecnologias, int maximopreguntas)
        {
            List<PreguntasDto> preguntasxtecnologia = this.Find(x => tecnologias.Contains(x.IdTecnologia.Value)).ToList();

            List<PreguntasDto> lstpreguntasrandom = preguntasxtecnologia.SeleccionarElementoAleatorio(maximopreguntas);

            return lstpreguntasrandom;
            //return preguntasxtecnologia;
        }
    }
}
