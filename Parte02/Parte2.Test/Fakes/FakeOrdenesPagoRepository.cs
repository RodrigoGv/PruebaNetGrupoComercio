using Microsoft.AspNetCore.Mvc.Rendering;
using Parte02.Models;
using System;
using System.Collections.Generic;

namespace Parte02.Test.Fakes
{
    public class FakeOrdenesPagoRepository : IOrdenesPagoRepository
    {
        public List<OrdenPago> OrdenesPago { get; set; }
        public FakeOrdenesPagoRepository()
        {
            OrdenesPago = new List<OrdenPago>();
        }

        private int nextCodigo(int banco, int sucursal)
        {
            return (OrdenesPago.Count == 0) ? 1 : OrdenesPago[OrdenesPago.Count - 1].Codigo;
        }

        public int Add(OrdenPago ordenPago)
        {
            ordenPago.Codigo = nextCodigo(ordenPago.CodBanco, ordenPago.CodSucursal);
            OrdenesPago.Add(ordenPago);
            return ordenPago.Codigo;
        }

        public OrdenPago Get(int codigo)
        {
            return OrdenesPago.Find(x => x.Codigo == codigo);
        }

        public List<OrdenPago> Search(int ordenpago, int banco, int sucursal, int moneda, int estado)
        {
            var lista = OrdenesPago.FindAll(x => (x.Codigo == ordenpago || ordenpago == 0) &&
                                            (x.CodBanco == banco || banco == 0) &&
                                            (x.CodSucursal == sucursal || sucursal == 0) &&
                                            (x.CodMoneda == moneda || moneda == 0) &&
                                            (x.CodEstado == estado || estado == 0));
            return lista;
        }

        public void Update(OrdenPago ordenPago)
        {
            var index = OrdenesPago.FindIndex(x => x.Codigo == ordenPago.Codigo);
            OrdenesPago[index].CodBanco = ordenPago.CodBanco;
            OrdenesPago[index].CodMoneda = ordenPago.CodMoneda;
            OrdenesPago[index].CodEstado = ordenPago.CodEstado;
            OrdenesPago[index].CodSucursal = ordenPago.CodSucursal;
            OrdenesPago[index].Estado = ordenPago.Estado;
            OrdenesPago[index].Moneda = ordenPago.Moneda;
            OrdenesPago[index].Monto = ordenPago.Monto;
            OrdenesPago[index].NomBanco = ordenPago.NomBanco;
            OrdenesPago[index].NomSucursal = ordenPago.NomSucursal;
        }

        public void ChangeState(OrdenPago ordenPago)
        {
        }

        public List<SelectListItem> getBancosDDL()
        {
            return new List<SelectListItem>();
        }

        public List<SelectListItem> getSucursalesDDL(int banco, bool obligatorio)
        {
            return new List<SelectListItem>();
        }

        public List<SelectListItem> getMonedasDDL()
        {
            return new List<SelectListItem>();
        }

        public List<SelectListItem> getEstadosDDL()
        {
            return new List<SelectListItem>();
        }

        public List<SelectListItem> getEstadosCambioDDL()
        {
            return new List<SelectListItem>();
        }
    }
}
