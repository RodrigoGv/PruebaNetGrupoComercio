using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parte02.Models
{
    public class OrdenPago
    {
        public OrdenPago() { }
        //public OrdenPago(int codigo, int codBanco, int codSucursal, int codMoneda, decimal monto, int codEstado)
        //{
        //    Codigo = codigo;
        //    CodBanco = codBanco;
        //    CodSucursal = codSucursal;
        //}
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

    public class OrdenPagoView
    {
        public List<SelectListItem> Bancos { get; set; }
        public List<SelectListItem> Sucursales { get; set; }
        public List<SelectListItem> Monedas { get; set; }
        public List<SelectListItem> Estados { get; set; }
        public List<OrdenPago> OrdenesPago { get; set; }
    }
    public class OrdenPagoEdit
    {
        public List<SelectListItem> Bancos { get; set; }
        public List<SelectListItem> Sucursales { get; set; }
        public List<SelectListItem> Monedas { get; set; }
        public List<SelectListItem> Estados { get; set; }
        public OrdenPago OrdenPago { get; set; }
    }
}
