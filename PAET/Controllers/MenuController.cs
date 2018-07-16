using PAET.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PAET.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        IVwEntrevistasActivasService _vwEntrevistasActivasService;
        public MenuController(IVwEntrevistasActivasService vwEntrevistasActivasService)
        {
            _vwEntrevistasActivasService = vwEntrevistasActivasService;
        }
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult Menu(int IdCandidato)
        {
            var entrevistaactiva = _vwEntrevistasActivasService.FindSingle(x => x.IdCandidato == IdCandidato);
            return View(entrevistaactiva);
        }
    }
}