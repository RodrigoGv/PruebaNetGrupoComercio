using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using apiParte02.AD;
using System;
using System.Collections.Generic;

namespace apiParte02.Models
{
    public class OrdenesPagoRepository : IOrdenesPagoRepository
    {
        private IDataContext _dataContext;
        public OrdenesPagoRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public List<OrdenPago> Search(int banco, int sucursal, int moneda)
        {
            List<OrdenPago> lista = new List<OrdenPago>();
            OrdenPago en;
            string q = "select a.codOrdenPago,a.codBanco,b.nombre Banco,a.codSucursal,c.nombre Sucursal,a.codMoneda,d.nombre Moneda,a.monto,a.codEstado,e.nombre Estado from elcomercio.ordenesPago a join elcomercio.bancos b on b.codBanco=a.codBanco join elcomercio.sucursales c on c.codBanco=a.codBanco and c.codSucursal=a.codSucursal join elcomercio.monedas d on d.codMoneda=a.codMoneda join elcomercio.estados e on e.codEstado=a.codEstado where 1=1";
            if (banco > 0) q += " and b.codBanco=" + banco;
            if (sucursal > 0) q += " and a.codSucursal=" + sucursal;
            if (moneda > 0) q += " and a.codMoneda=" + moneda;
            q += " order by codOrdenPago desc";
            using (MySqlConnection conn = _dataContext.GetConnection())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand(q, conn);
                using (var r = c.ExecuteReader())
                {
                    while (r.Read())
                    {
                        en = new OrdenPago();
                        en.Codigo = Convert.ToInt32(r["codOrdenPago"]);
                        en.CodBanco = Convert.ToInt32(r["codBanco"]);
                        en.NomBanco = r["Banco"].ToString();
                        en.CodSucursal = Convert.ToInt32(r["codSucursal"]);
                        en.NomSucursal = r["Sucursal"].ToString();
                        en.CodMoneda = Convert.ToInt32(r["codMoneda"]);
                        en.Moneda = r["Moneda"].ToString();
                        en.Monto = Convert.ToDecimal(r["monto"]);
                        en.CodEstado = Convert.ToInt32(r["codEstado"]);
                        en.Estado = r["Estado"].ToString();
                        lista.Add(en);
                    }
                }
            }
            return lista;
        }
    }
}
