using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parte02.Models
{
    public class Banco
    {
        public Banco() { }
        public Banco(int codigo, string nombre, string direccion)
        {
            Codigo = codigo;
            Nombre = nombre;
            Direccion = direccion;
        }
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public DateTime Fecha { get; set; }
    }
}
