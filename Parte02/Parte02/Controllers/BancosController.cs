using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Parte02.Models;
using Parte02.AD;

namespace Parte02.Controllers
{
    [SesionTerminada]
    public class BancosController : Controller
    {
        IBancosRepository _bancosRepository;
        public BancosController(IBancosRepository bancosRepository)
        {
            _bancosRepository = bancosRepository;
        }
        public IActionResult Bancos()
        {
            return View();
        }
        public IActionResult search(int codigoFiltro, string textoFiltro)
        {
            List<Banco> lista = _bancosRepository.Search(codigoFiltro, textoFiltro);
            return View("Bancos", lista);
        }
        public IActionResult edit(int id)
        {
            Banco en = id > 0 ? _bancosRepository.Get(id) : new Banco(id, "", "");
            return View("BancosEdit", en);
        }
        [HttpPost]
        public IActionResult save([Bind("Codigo,Nombre,Direccion")] Banco banco)
        {
            if (banco.Codigo == 0) banco.Codigo = _bancosRepository.Add(banco);
            else _bancosRepository.Update(banco);
            return search(banco.Codigo, "");
        }
    }
}