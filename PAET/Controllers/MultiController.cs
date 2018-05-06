using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PAET.Controllers
{
    [Authorize]
    public class MultiController : Controller
    {
        // GET: Multi
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Multi()
        {
            return View();
        }
    }
}