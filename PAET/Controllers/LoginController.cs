using PAET.Comun;
using PAET.DominioBase.Entidades_Dominio;
using PAET.Models;
using PAET.Services.Interfaces;
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
        ICandidatosService _candidatoService;

        public LoginController(ICandidatosService candidatoService)
        {
            _candidatoService = candidatoService;
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Sigin()
        {
            Session.Clear();
            LoginViewModel login = new LoginViewModel();
            login.AccesoCorrecto = true;
            return View(login);
        }
        public ActionResult AccesoInvalido(LoginViewModel acceso)
        {
            return View("Sigin",acceso);
        }
        [HttpPost]
        public ActionResult ComprobarAcceso(FormCollection form)
        {
            ResultadoAccion resultado;
            if (ModelState.IsValid)
            {
                resultado = _candidatoService.AccesoCorrecto(form["txtusuario"], form["txtpwd"]);
                if (resultado.ResultCode == ResultadoAccion.CodigoResultado.OK)
                return RedirectToAction("Menu", "Menu");
                else
                {
                    LoginViewModel login = new LoginViewModel();
                    login.AccesoCorrecto = false;
                    login.MensajeError = resultado.ResultMsg;
                    return RedirectToAction("AccesoInvalido", "Login", login);
                }
            }
            return View();
        }
    }
}