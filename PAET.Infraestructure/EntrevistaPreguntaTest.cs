//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PAET.Infraestructure
{
    using System;
    using System.Collections.Generic;
    
    public partial class EntrevistaPreguntaTest
    {
        public int IdEntrevistaPreguntaRespuesta { get; set; }
        public Nullable<int> IdEntrevistaPregunta { get; set; }
        public Nullable<int> IdPregunta { get; set; }
        public Nullable<int> IdRespuesta { get; set; }
    
        public virtual EntrevistaPregunta EntrevistaPregunta { get; set; }
        public virtual Preguntas Preguntas { get; set; }
        public virtual Respuestas Respuestas { get; set; }
    }
}