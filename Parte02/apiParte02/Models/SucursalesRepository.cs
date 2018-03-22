using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using apiParte02.AD;
using System;
using System.Collections.Generic;

namespace apiParte02.Models
{
    public class SucursalesRepository : ISucursalesRepository
    {
        private IDataContext _dataContext;
        public SucursalesRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public List<Sucursal> Search(int banco)
        {
            List<Sucursal> lista = new List<Sucursal>();
            Sucursal en;
            string q = "select a.codSucursal,a.nombre,a.direccion from elcomercio.sucursales a where a.codBanco=" + banco;
            q += " order by codSucursal desc";
            using (MySqlConnection conn = _dataContext.GetConnection())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand(q, conn);
                using (var r = c.ExecuteReader())
                {
                    while (r.Read())
                    {
                        en = new Sucursal();
                        en.Codigo = Convert.ToInt32(r["codSucursal"]);
                        en.Nombre = r["nombre"].ToString();
                        en.Direccion = r["direccion"].ToString();
                        lista.Add(en);
                    }
                }
            }
            return lista;
        }
    }
}
