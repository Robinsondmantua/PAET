using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.DominioBase.Contracts
{
    public interface IHistoricoEntity
    {
        int HistoricoId { get; set; }
        Nullable<int> TipoOperacion { get; set; }
        Nullable<System.DateTime> FechaOperacion { get; set; }
        string UsuarioOperacion { get; set; }
    }
}
