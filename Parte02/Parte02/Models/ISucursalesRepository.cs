using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Parte02.Models
{
    public interface ISucursalesRepository
    {
        List<Sucursal> Search(int banco, int codigo, string nombre);
        int Add(Sucursal banco);
        void Update(Sucursal banco);
        Sucursal Get(int banco, int codigo);
        List<SelectListItem> getBancosDDL();
    }
}
