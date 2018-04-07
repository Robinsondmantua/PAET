using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace PAET.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Sigin()
        {
            Session.Clear();
            return View();
        }
        [HttpPost]
        public ActionResult ComprobarAcceso(FormCollection form)
        {
            return RedirectToAction("Menu", "Menu");
        }
    }
}