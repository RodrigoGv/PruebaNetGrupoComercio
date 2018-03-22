using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parte02.Models
{
    public class Sucursal
    {
        public Sucursal() { }
        public Sucursal(int codBanco, string nomBanco, int codigo, string nombre, string direccion)
        {
            CodBanco = codBanco;
            NomBanco = nomBanco;
            Codigo = codigo;
            Nombre = nombre;
            Direccion = direccion;
        }
        public int CodBanco { get; set; }
        public string NomBanco { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class SucursalView
    {
        public List<SelectListItem> bancos { get; set; }
        public List<Sucursal> sucursales { get; set; }
    }
    public class SucursalEdit
    {
        public List<SelectListItem> bancos { get; set; }
        public Sucursal sucursal { get; set; }
    }
}
