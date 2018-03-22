using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apiParte02.Models
{
    public class OrdenPago
    {
        public int CodBanco { get; set; }
        public string NomBanco { get; set; }
        public int CodSucursal { get; set; }
        public string NomSucursal { get; set; }
        public int Codigo { get; set; }
        public decimal Monto { get; set; }
        public int CodMoneda { get; set; }
        public string Moneda { get; set; }
        public int CodEstado { get; set; }
        public string Estado { get; set; }
    }
}
