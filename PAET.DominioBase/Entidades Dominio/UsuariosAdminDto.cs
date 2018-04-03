using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAET.DominioBase.Entidades_Dominio
{
    public class UsuariosAdminDto
    {
        public int IdUsuario { get; set; }
        public Nullable<int> IdRol { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }

        public RolDto Rol { get; set; }
    }
}
