using MySql.Data.MySqlClient;
using Parte02.AD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parte02.Models
{
    public class BancosRepository : IBancosRepository
    {
        private IDataContext _dataContext;
        public BancosRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        private int nextCodigo()
        {
            int codigo = 0;
            using (MySqlConnection conn = _dataContext.GetConnection())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand("select Coalesce(Null,Max(codBanco),0)+1 from elcomercio.bancos", conn);
                codigo = Convert.ToInt32(c.ExecuteScalar());
            }
            return codigo;
        }
        public int Add(Banco banco)
        {
            banco.Codigo = nextCodigo();
            using (MySqlConnection conn = _dataContext.GetConnection())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand("insert into elcomercio.bancos(codBanco,nombre,direccion)values(?codBanco,?nombre,?direccion)", conn);
                c.Parameters.Add("?codBanco", MySqlDbType.Int32).Value = banco.Codigo;
                c.Parameters.Add("?nombre", MySqlDbType.VarChar).Value = banco.Nombre;
                c.Parameters.Add("?direccion", MySqlDbType.VarChar).Value = banco.Direccion;
                c.ExecuteNonQuery();
            }
            return banco.Codigo;
        }

        public Banco Get(int codigo)
        {
            Banco en = null;
            string q = "select codBanco,nombre,direccion from elcomercio.bancos where codBanco=" + codigo;
            using (MySqlConnection conn = _dataContext.GetConnection())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand(q, conn);
                using (var r = c.ExecuteReader())
                {
                    while (r.Read())
                    {
                        en = new Banco();
                        en.Codigo = Convert.ToInt32(r["codBanco"]);
                        en.Nombre = r["nombre"].ToString();
                        en.Direccion = r["direccion"].ToString();
                    }
                }
            }
            return en;
        }

        public List<Banco> Search(int id, string nombre)
        {
            List<Banco> lista = new List<Banco>();
            Banco en;
            string q = "select codBanco,nombre,direccion from elcomercio.bancos";
            if (id > 0) q += " where codBanco=" + id;
            else if (!string.IsNullOrWhiteSpace(nombre)) q += String.Format(" where nombre like'%{0}%'", nombre.ToUpper());
            q += " order by codBanco desc";
            using (MySqlConnection conn = _dataContext.GetConnection())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand(q, conn);
                using (var r = c.ExecuteReader())
                {
                    while (r.Read())
                    {
                        en = new Banco();
                        en.Codigo = Convert.ToInt32(r["codBanco"]);
                        en.Nombre = r["nombre"].ToString();
                        en.Direccion = r["direccion"].ToString();
                        lista.Add(en);
                    }
                }
            }
            return lista;
        }

        public void Update(Banco banco)
        {
            using (MySqlConnection conn = _dataContext.GetConnection())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand("update elcomercio.bancos set nombre=?nombre,direccion=?direccion where codBanco=?codBanco", conn);
                c.Parameters.Add("?codBanco", MySqlDbType.Int32).Value = banco.Codigo;
                c.Parameters.Add("?nombre", MySqlDbType.VarChar).Value = banco.Nombre;
                c.Parameters.Add("?direccion", MySqlDbType.VarChar).Value = banco.Direccion;
                c.ExecuteNonQuery();
            }
        }
    }
}
