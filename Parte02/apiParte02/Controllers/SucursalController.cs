using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiParte02.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiParte02.Controllers
{
    [Route("api/[controller]")]
    public class SucursalController : Controller
    {
        ISucursalesRepository _sucursalesRepository;
        public SucursalController(ISucursalesRepository sucursalesRepository)
        {
            _sucursalesRepository = sucursalesRepository;
        }
        [HttpGet("{banco}")]
        public IEnumerable<Sucursal> Get(int banco)
        {
            return _sucursalesRepository.Search(banco);
        }
    }
}
