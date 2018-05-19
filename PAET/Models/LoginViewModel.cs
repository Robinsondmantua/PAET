using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PAET.Models
{
    public class LoginViewModel
    {
        public bool AccesoCorrecto { get; set; }
        public string MensajeError { get; set; }
        public string Usuario { get; set; }
    }
}