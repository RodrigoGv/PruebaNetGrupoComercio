using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace apiParte02.Models
{
    public interface ISucursalesRepository
    {
        List<Sucursal> Search(int banco);
    }
}
