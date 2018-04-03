using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.DominioBase.Entidades_Dominio
{
    public class RolDto
    {
        public int IdRol { get; set; }
        public string Descripcion { get; set; }

        public IEnumerable<UsuariosAdminDto> UsuariosAdmin { get; set; }
    }
}
