using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace apiParte02.Models
{
    public interface IOrdenesPagoRepository
    {
        List<OrdenPago> Search(int banco, int sucursal, int moneda);
    }
}
