using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Parte02.Models;

namespace Parte02.Controllers
{
    [SesionTerminada]
    public class OrdenesPagoController : Controller
    {
        IOrdenesPagoRepository _ordenesPagoRepository;
        public OrdenesPagoController(IOrdenesPagoRepository ordenesPagoRepository)
        {
            _ordenesPagoRepository = ordenesPagoRepository;
        }
        public IActionResult OrdenesPago()
        {
            OrdenPagoView en = new OrdenPagoView {
                Bancos = _ordenesPagoRepository.getBancosDDL(),
                Sucursales = new List<SelectListItem>(),
                Monedas = _ordenesPagoRepository.getMonedasDDL(),
                Estados = _ordenesPagoRepository.getEstadosDDL()
            };
            return View(en);
        }
        [HttpGet]
        public IActionResult search(int ordenpago, int banco, int sucursal, int moneda, int estado)
        {
            OrdenPagoView en = new OrdenPagoView { OrdenesPago = _ordenesPagoRepository.Search(ordenpago, banco, sucursal, moneda, estado) };
            return PartialView("_OrdenesPagoGrid", en);
        }
        public IActionResult edit(int id)
        {
            OrdenPagoEdit en = new OrdenPagoEdit();
            en.Bancos = _ordenesPagoRepository.getBancosDDL();
            en.Sucursales = new List<SelectListItem>();
            en.Monedas = _ordenesPagoRepository.getMonedasDDL();
            en.OrdenPago = id > 0 ? _ordenesPagoRepository.Get(id) : new OrdenPago();
            en.Estados = en.OrdenPago.CodEstado > 1 ? _ordenesPagoRepository.getEstadosCambioDDL() : _ordenesPagoRepository.getEstadosDDL();
            en.Sucursales = en.OrdenPago.CodBanco > 0 ? _ordenesPagoRepository.getSucursalesDDL(en.OrdenPago.CodBanco, true) : new List<SelectListItem> { new SelectListItem { Value = "0", Text = "--SELECCIONE--" } };
            return View("OrdenPagoEdit", en);
        }
        [HttpPost]
        public IActionResult save([Bind("Codigo,CodBanco,CodSucursal,CodMoneda,Monto,CodEstado")] OrdenPago ordenPago)
        {
            if (ordenPago.Codigo == 0) ordenPago.Codigo = _ordenesPagoRepository.Add(ordenPago);
            else _ordenesPagoRepository.Update(ordenPago);
            OrdenPagoView en = new OrdenPagoView
            {
                Bancos = _ordenesPagoRepository.getBancosDDL(),
                Sucursales = new List<SelectListItem>(),
                Monedas = _ordenesPagoRepository.getMonedasDDL(),
                Estados = _ordenesPagoRepository.getEstadosDDL(),
                OrdenesPago = _ordenesPagoRepository.Search(ordenPago.Codigo, 0, 0, 0, 0)
            };
            return View("OrdenesPago", en);
        }
        [HttpGet]
        public JsonResult ddlSucursales(int banco, bool obligatorio)
        {
            return Json(_ordenesPagoRepository.getSucursalesDDL(banco, obligatorio));
        }
    }
}