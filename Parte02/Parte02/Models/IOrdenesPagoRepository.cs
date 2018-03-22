using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Parte02.Models
{
    public interface IOrdenesPagoRepository
    {
        List<OrdenPago> Search(int ordenpago, int banco, int sucursal, int moneda, int estado);
        int Add(OrdenPago ordenPago);
        void Update(OrdenPago ordenPago);
        void ChangeState(OrdenPago ordenPago);
        OrdenPago Get(int codigo);
        List<SelectListItem> getBancosDDL();
        List<SelectListItem> getSucursalesDDL(int banco, bool obligatorio);
        List<SelectListItem> getMonedasDDL();
        List<SelectListItem> getEstadosDDL();
        List<SelectListItem> getEstadosCambioDDL();
    }
}
