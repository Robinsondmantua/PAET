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
    
    public partial class ChatEntrevistaCandidato
    {
        public int IdChat { get; set; }
        public int IdEntrevista { get; set; }
        public int IdCandidato { get; set; }
        public Nullable<int> IdValoracion { get; set; }
    
        public virtual Candidatos Candidatos { get; set; }
        public virtual Chat Chat { get; set; }
        public virtual Entrevistas Entrevistas { get; set; }
    }
}
