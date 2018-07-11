using PAET.Cache.Cache;
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
using System.Web.Security;

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
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [NoCache]
        public ActionResult Sigin()
        {
            if (User.Identity.IsAuthenticated) FormsAuthentication.SignOut();
            LoginViewModel login = new LoginViewModel();
            login.AccesoCorrecto = true;
            return View(login);
        }
        [AllowAnonymous]
        public ActionResult AccesoInvalido(LoginViewModel acceso)
        {
            //Validamos si el usuario ha intentado hacer varios intentos. Al tercero o superior, 
            //el usuario quedará bloqueado hasta que el administrador lo desbloquee.
            return View("Sigin",acceso);
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ComprobarAcceso(FormCollection form)
        {
            ResultadoAccion<CandidatosDto> resultado;
            if (ModelState.IsValid)
            {

                resultado = _candidatoService.ComprobarAccesoCorrecto(form["txtusuario"], form["txtpwd"]);
                if (resultado.ResultCode == ResultadoAccion.CodigoResultado.OK)
                {
                    string NombreCompleto = resultado.Entidad.Nombre + " " + resultado.Entidad.Apellido1 + " " + resultado.Entidad.Apellido2;
                    FormsAuthentication.SetAuthCookie(NombreCompleto, false);
                    return RedirectToAction("Menu", "Menu");
                }
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
        [Authorize]
        public ActionResult SignOut()
        {
            //Cierre de sesión del usuario.
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Sigin");
        }
    }
}