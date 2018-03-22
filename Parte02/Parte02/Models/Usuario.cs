using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parte02.Models
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string ApePaterno { get; set; }
        public string ApeMaterno { get; set; }
        public int Rol { get; set; }
        public int UsuarioCod { get; set; }
    }
    public class Login
    {
        public string Usuario { get; set; }
        public string Contrasenia { get; set; }
        public string Mensaje { get; set; }
    }
}
