using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiParte02.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiParte02.Controllers
{
    [Route("api/[controller]")]
    public class OrdenPagoController : Controller
    {
        IOrdenesPagoRepository _ordenesPagoRepository;
        public OrdenPagoController(IOrdenesPagoRepository ordenesPagoRepository)
        {
            _ordenesPagoRepository = ordenesPagoRepository;
        }
        [HttpGet("{banco}/{sucursal}/{moneda}")]
        public IEnumerable<OrdenPago> Get(int banco, int sucursal, int moneda)
        {
            return _ordenesPagoRepository.Search(banco, sucursal, moneda);
        }
    }
}
