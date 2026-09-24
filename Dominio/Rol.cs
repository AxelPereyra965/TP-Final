using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    [Serializable]
    public class Rol
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public Rol() { }
    }
}
