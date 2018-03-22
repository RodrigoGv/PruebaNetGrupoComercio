using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Parte02.Models;

namespace Parte02.Controllers
{
    [SesionTerminada]
    public class SucursalesController : Controller
    {
        ISucursalesRepository _sucursalesRepository;
        public SucursalesController(ISucursalesRepository sucursalRepository)
        {
            _sucursalesRepository = sucursalRepository;
        }
        public IActionResult Sucursales()
        {
            List<SelectListItem> lista = _sucursalesRepository.getBancosDDL();
            SucursalView en = new SucursalView { bancos = lista };
            return View(en);
        }
        [HttpGet]
        public IActionResult search(int codigoBancoFiltro, int codigoFiltro, string textoFiltro)
        {
            List<Sucursal> lista = _sucursalesRepository.Search(codigoBancoFiltro, codigoFiltro, (textoFiltro??"").ToUpper());
            SucursalView en = new SucursalView { sucursales = lista };
            return PartialView("_SucursalesGrid", en);
            //return View("Sucursales");
        }
        public IActionResult edit(int banco, int sucursal)
        {
            SucursalEdit en = new SucursalEdit();
            en.bancos = _sucursalesRepository.getBancosDDL();
            en.sucursal = sucursal > 0 ? _sucursalesRepository.Get(banco, sucursal) : new Sucursal(banco, "", sucursal, "", "");
            return View("SucursalesEdit", en);
        }
        [HttpPost]
        public IActionResult save([Bind("CodBanco,Codigo,Nombre,Direccion")] Sucursal sucursal)
        {
            if (sucursal.Codigo == 0) sucursal.Codigo = _sucursalesRepository.Add(sucursal);
            else _sucursalesRepository.Update(sucursal);
            SucursalView en = new SucursalView
            {
                bancos = _sucursalesRepository.getBancosDDL(),
                sucursales = _sucursalesRepository.Search(sucursal.CodBanco, sucursal.Codigo, "")
            };
            return View("Sucursales", en);
        }
    }
}