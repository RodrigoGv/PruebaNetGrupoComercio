using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using Parte02.AD;
using System;
using System.Collections.Generic;

namespace Parte02.Models
{
    public class SucursalesRepository : ISucursalesRepository
    {
        private IDataContext _dataContext;
        public SucursalesRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private int nextCodigo(int banco)
        {
            int codigo = 0;
            using (MySqlConnection conn = _dataContext.GetConnection())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand("select Coalesce(Null,Max(codSucursal),0)+1 from elcomercio.sucursales where codBanco=?codBanco", conn);
                c.Parameters.Add("codBanco", MySqlDbType.Int32).Value = banco;
                codigo = Convert.ToInt32(c.ExecuteScalar());
            }
            return codigo;
        }
        public int Add(Sucursal sucursal)
        {
            sucursal.Codigo = nextCodigo(sucursal.CodBanco);
            using (MySqlConnection conn = _dataContext.GetConnection())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand("insert into elcomercio.sucursales(codBanco,codSucursal,nombre,direccion)values(?codBanco,?codSucursal,?nombre,?direccion)", conn);
                c.Parameters.Add("?codBanco", MySqlDbType.Int32).Value = sucursal.CodBanco;
                c.Parameters.Add("?codSucursal", MySqlDbType.Int32).Value = sucursal.Codigo;
                c.Parameters.Add("?nombre", MySqlDbType.VarChar).Value = sucursal.Nombre.ToUpper();
                c.Parameters.Add("?direccion", MySqlDbType.VarChar).Value = sucursal.Direccion.ToUpper();
                c.ExecuteNonQuery();
            }
            return sucursal.Codigo;
        }

        public Sucursal Get(int banco, int codigo)
        {
            Sucursal en = null;
            string q = "select codBanco,codSucursal,nombre,direccion from elcomercio.sucursales where codBanco=" + banco + " and codSucursal=" + codigo;
            using (MySqlConnection conn = _dataContext.GetConnection())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand(q, conn);
                using (var r = c.ExecuteReader())
                {
                    while (r.Read())
                    {
                        en = new Sucursal();
                        en.CodBanco = Convert.ToInt32(r["codBanco"]);
                        en.Codigo = Convert.ToInt32(r["codSucursal"]);
                        en.Nombre = r["nombre"].ToString();
                        en.Direccion = r["direccion"].ToString();
                    }
                }
            }
            return en;
        }

        public List<Sucursal> Search(int banco, int id, string nombre)
        {
            List<Sucursal> lista = new List<Sucursal>();
            Sucursal en;
            string q = "select a.codBanco,b.nombre NomBanco,a.codSucursal,a.nombre,a.direccion from elcomercio.sucursales a join elcomercio.bancos b on b.codBanco=a.codBanco where 1=1";
            if (banco > 0) q += " and b.codBanco=" + banco;
            if (id > 0) q += " and a.codSucursal=" + id;
            else if (!string.IsNullOrWhiteSpace(nombre)) q += String.Format(" and a.nombre like'%{0}%'", nombre.ToUpper());
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
                        en.CodBanco = Convert.ToInt32(r["codBanco"]);
                        en.NomBanco = r["NomBanco"].ToString();
                        en.Codigo = Convert.ToInt32(r["codSucursal"]);
                        en.Nombre = r["nombre"].ToString();
                        en.Direccion = r["direccion"].ToString();
                        lista.Add(en);
                    }
                }
            }
            return lista;
        }

        public void Update(Sucursal sucursal)
        {
            using (MySqlConnection conn = _dataContext.GetConnection())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand("update elcomercio.sucursales set nombre=?nombre,direccion=?direccion where codBanco=?codBanco and codSucursal=?codSucursal", conn);
                c.Parameters.Add("?codBanco", MySqlDbType.Int32).Value = sucursal.CodBanco;
                c.Parameters.Add("?codSucursal", MySqlDbType.Int32).Value = sucursal.Codigo;
                c.Parameters.Add("?nombre", MySqlDbType.VarChar).Value = sucursal.Nombre.ToUpper();
                c.Parameters.Add("?direccion", MySqlDbType.VarChar).Value = sucursal.Direccion.ToUpper();
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
    }
}
