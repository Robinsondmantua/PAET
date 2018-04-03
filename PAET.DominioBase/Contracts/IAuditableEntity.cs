using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.DominioBase.Contracts
{
    public interface IAuditableEntity
    {
        string UsuarioCreacion { get; set; }
        Nullable<System.DateTime> FechaCreacion { get; set; }
        string UsuarioModificacion { get; set; }
        Nullable<System.DateTime> FechaModificacion { get; set; }
    }
}
