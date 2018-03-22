using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using Parte02.AD;
using System;
using System.Collections.Generic;

namespace Parte02.Models
{
    public class OrdenesPagoRepository : IOrdenesPagoRepository
    {
        private IDataContext _dataContext;
        public OrdenesPagoRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private int nextCodigo(int banco, int sucursal)
        {
            int codigo = 0;
            using (MySqlConnection conn = _dataContext.GetConnection())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand("select Coalesce(Null,Max(codOrdenPago),0)+1 from elcomercio.ordenespago", conn);
                codigo = Convert.ToInt32(c.ExecuteScalar());
            }
            return codigo;
        }
        public int Add(OrdenPago ordenPago)
        {
            ordenPago.Codigo = nextCodigo(ordenPago.CodBanco, ordenPago.CodSucursal);
            using (MySqlConnection conn = _dataContext.GetConnection())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand("INSERT INTO elcomercio.ordenespago(codOrdenPago,codBanco,codSucursal,monto,codMoneda,codEstado)VALUES(?codOrdenPago,?codBanco,?codSucursal,?monto,?codMoneda,?codEstado)", conn);
                c.Parameters.Add("?codOrdenPago", MySqlDbType.Int32).Value = ordenPago.Codigo;
                c.Parameters.Add("?codBanco", MySqlDbType.Int32).Value = ordenPago.CodBanco;
                c.Parameters.Add("?codSucursal", MySqlDbType.Int32).Value = ordenPago.CodSucursal;
                c.Parameters.Add("?monto", MySqlDbType.Decimal).Value = ordenPago.Monto;
                c.Parameters.Add("?codMoneda", MySqlDbType.Int32).Value = ordenPago.CodMoneda;
                c.Parameters.Add("?codEstado", MySqlDbType.Int32).Value = 1;
                c.ExecuteNonQuery();
            }
            return ordenPago.Codigo;
        }

        public OrdenPago Get(int codigo)
        {
            OrdenPago en = null;
            string q = "select codOrdenPago,codBanco,codSucursal,monto,codMoneda,codEstado from elcomercio.ordenesPago where codOrdenPago=" + codigo;
            using (MySqlConnection conn = _dataContext.GetConnection())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand(q, conn);
                using (var r = c.ExecuteReader())
                {
                    while (r.Read())
                    {
                        en = new OrdenPago();
                        en.CodBanco = Convert.ToInt32(r["codBanco"]);
                        en.CodSucursal = Convert.ToInt32(r["codSucursal"]);
                        en.Codigo = Convert.ToInt32(r["codOrdenPago"]);
                        en.Monto = Convert.ToDecimal(r["monto"]);
                        en.CodMoneda = Convert.ToInt32(r["codMoneda"]);
                        en.CodEstado = Convert.ToInt32(r["codEstado"]);
                    }
                }
            }
            return en;
        }

        public List<OrdenPago> Search(int ordenpago, int banco, int sucursal, int moneda, int estado)
        {
            List<OrdenPago> lista = new List<OrdenPago>();
            OrdenPago en;
            string q = "select a.codOrdenPago,a.codBanco,b.nombre Banco,a.codSucursal,c.nombre Sucursal,a.codMoneda,d.nombre Moneda,a.monto,a.codEstado,e.nombre Estado from elcomercio.ordenesPago a join elcomercio.bancos b on b.codBanco=a.codBanco join elcomercio.sucursales c on c.codBanco=a.codBanco and c.codSucursal=a.codSucursal join elcomercio.monedas d on d.codMoneda=a.codMoneda join elcomercio.estados e on e.codEstado=a.codEstado where 1=1";
            if (ordenpago > 0) q += " and codOrdenPago=" + ordenpago;
            else
            {
                if (banco > 0) q += " and b.codBanco=" + banco;
                if (sucursal > 0) q += " and a.codSucursal=" + sucursal;
                if (moneda > 0) q += " and a.codMoneda=" + moneda;
                if (estado > 0) q += " and a.codEstado=" + estado;
            }
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
        
        public void Update(OrdenPago ordenPago)
        {
            using (MySqlConnection conn = _dataContext.GetConnection())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand("update elcomercio.ordenespago set codBanco=?codBanco,codSucursal=?codSucursal,monto=?monto,codMoneda=?codMoneda,codEstado=?codEstado where codOrdenPago=?codOrdenPago", conn);
                c.Parameters.Add("?codOrdenPago", MySqlDbType.Int32).Value = ordenPago.Codigo;
                c.Parameters.Add("?codBanco", MySqlDbType.Int32).Value = ordenPago.CodBanco;
                c.Parameters.Add("?codSucursal", MySqlDbType.Int32).Value = ordenPago.CodSucursal;
                c.Parameters.Add("?monto", MySqlDbType.Decimal).Value = ordenPago.Monto;
                c.Parameters.Add("?codMoneda", MySqlDbType.Int32).Value = ordenPago.CodMoneda;
                c.Parameters.Add("?codEstado", MySqlDbType.Int32).Value = ordenPago.CodEstado;
                c.ExecuteNonQuery();
            }
        }

        public void ChangeState(OrdenPago ordenPago)
        {
            using (MySqlConnection conn = _dataContext.GetConnection())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand("update elcomercio.ordenespago set codSucursal=?codSucursal monto=?monto,codMoneda=?codMoneda codEstado=?codEstado where codOrdenPago=?codOrdenPago", conn);
                c.Parameters.Add("?codOrdenPago", MySqlDbType.Int32).Value = ordenPago.Codigo;
                c.Parameters.Add("?codSucursal", MySqlDbType.Int32).Value = ordenPago.CodSucursal;
                c.Parameters.Add("?monto", MySqlDbType.Decimal).Value = ordenPago.Monto;
                c.Parameters.Add("?codMoneda", MySqlDbType.Int32).Value = ordenPago.CodMoneda;
                c.Parameters.Add("?codEstado", MySqlDbType.Int32).Value = ordenPago.CodEstado;
                c.ExecuteNonQuery();
            }
        }

        public List<SelectListItem> getBancosDDL()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            using (MySqlConnection conn = _dataContext.GetConnection())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand("select codBanco,nombre from elcomercio.bancos order by nombre asc", conn);
                using (var r = c.ExecuteReader())
                {
                    while (r.Read()) lista.Add(new SelectListItem { Value = r["codBanco"].ToString(), Text = r["nombre"].ToString() });
                }
            }
            return lista;
        }

        public List<SelectListItem> getSucursalesDDL(int banco, bool obligatorio)
        {
            List<SelectListItem> lista = new List<SelectListItem> { new SelectListItem { Value = "0", Text = String.Format("--{0}--", obligatorio ? "SELECCIONE" : "TODOS") } };
            using (MySqlConnection conn = _dataContext.GetConnection())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand("select codSucursal,nombre from elcomercio.sucursales where codBanco=?codBanco order by nombre asc", conn);
                c.Parameters.Add("codBanco", MySqlDbType.Int32).Value = banco;
                using (var r = c.ExecuteReader())
                {
                    while (r.Read()) lista.Add(new SelectListItem { Value = r["codSucursal"].ToString(), Text = r["nombre"].ToString() });
                }
            }
            return lista;
        }

        public List<SelectListItem> getMonedasDDL()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            using (MySqlConnection conn = _dataContext.GetConnection())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand("select codMoneda,abreviatura from elcomercio.monedas order by abreviatura asc", conn);
                using (var r = c.ExecuteReader())
                {
                    while (r.Read()) lista.Add(new SelectListItem { Value = r["codMoneda"].ToString(), Text = r["abreviatura"].ToString() });
                }
            }
            return lista;
        }

        public List<SelectListItem> getEstadosDDL()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            using (MySqlConnection conn = _dataContext.GetConnection())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand("select codEstado,nombre from elcomercio.estados order by nombre asc", conn);
                using (var r = c.ExecuteReader())
                {
                    while (r.Read()) lista.Add(new SelectListItem { Value = r["codEstado"].ToString(), Text = r["nombre"].ToString() });
                }
            }
            return lista;
        }

        public List<SelectListItem> getEstadosCambioDDL()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            using (MySqlConnection conn = _dataContext.GetConnection())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand("select codBanco,nombre from elcomercio.bancos order by nombre asc", conn);
                using (var r = c.ExecuteReader())
                {
                    while (r.Read()) lista.Add(new SelectListItem { Value = r["codBanco"].ToString(), Text = r["nombre"].ToString() });
                }
            }
            return lista;
        }
    }
}
