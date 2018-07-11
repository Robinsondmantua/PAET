using AutoMapper;
using PAET.DominioBase.Entidades_Dominio;
using PAET.Models;
using PAET.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PAET.Controllers.Administracion
{
    public class AdministracionController : Controller
    {
        // GET: Administracion
        IPreguntasService _preguntasService;
        IValoracionService _valoracionService;
        ITipoPreguntaService _tipoPreguntaService;
        IDificultadService _dificultadService;
        ITecnologiaService _tecnologiaService;
        IEntrevistaService _entrevistaService;
        ICandidatosService _candidatosService;
        IEntrevistaCandidatoService _entrevistaCandidatoService;

        public AdministracionController(IPreguntasService preguntasService, 
            ITecnologiaService tecnologiaservice,
            IValoracionService valoracionService,
            ITipoPreguntaService tipoPreguntaService,
            IDificultadService dificultadService,
            IEntrevistaService entrevistaService,
            ICandidatosService candidatosService,
            IEntrevistaCandidatoService entrevistaCandidatoService)
            {
            _preguntasService = preguntasService;
            _tecnologiaService = tecnologiaservice;
            _tipoPreguntaService = tipoPreguntaService;
            _dificultadService = dificultadService;
            _valoracionService = valoracionService;
            _entrevistaService = entrevistaService;
            _candidatosService = candidatosService;
            _entrevistaCandidatoService = entrevistaCandidatoService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Menu()
        {
            return View();
        }
        public ActionResult Preguntas()
        {
            IEnumerable<PreguntasDto> preguntas = _preguntasService.GetAll();
            return View(preguntas);
        }
        public ActionResult ProcesosCandidato()
        {
            ValoracionCandidatosViewModel valoracion = new ValoracionCandidatosViewModel();
            valoracion.Entrevistas  = _entrevistaService.GetAll();
            valoracion.EntrevistaCandidato = _entrevistaCandidatoService.ObtenerCandidatosEntrevistaNombre(1);
             return View(valoracion); 
        }
        public ActionResult VerDetalleProceso(int idEntrevista)
        {
              return View();
        }
        public ActionResult EditarPreguntas(int idPregunta)
        {
            PreguntasViewModel pregunta = Mapper.Map<PreguntasViewModel>(_preguntasService.FindSingle(x=>x.IdPregunta == idPregunta));
            if (pregunta != null)
            {
                pregunta.Tecnologias = Mapper.Map<IEnumerable<GeneralDto>>(_tecnologiaService.GetAll());
                pregunta.TipoPregunta = Mapper.Map<IEnumerable<GeneralDto>>(_tipoPreguntaService.GetAll());
                pregunta.Valoracion = Mapper.Map<IEnumerable<GeneralDto>>(_valoracionService.GetAll());
                pregunta.Dificultad = Mapper.Map<IEnumerable<GeneralDto>>(_dificultadService.GetAll());
                return View(pregunta);
            }
            else return View(); //A la vista de error.

        }
        public ActionResult TablasMaestras()
        {
            return View();
        }
        public ActionResult MenuTablasMaestras()
        {
            return View();
        }
    }
}