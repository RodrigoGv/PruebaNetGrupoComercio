using MySql.Data.MySqlClient;
using Parte02.AD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parte02.Models
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private IDataContext _dataContext;
        public UsuariosRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Usuario login(string usuario, string contrasenia)
        {
            Usuario en = null;
            using (MySqlConnection conn = _dataContext.GetConnection())
            {
                conn.Open();
                MySqlCommand c = new MySqlCommand("select * from elcomercio.usuarios where usuario=?usuario and contrasenia=?contrasenia", conn);
                c.Parameters.Add("?usuario", MySqlDbType.VarChar).Value = usuario.ToUpper();
                c.Parameters.Add("?contrasenia", MySqlDbType.VarChar).Value = contrasenia;
                using (var r = c.ExecuteReader())
                {
                    while (r.Read())
                    {
                        en = new Usuario()
                        {
                            UsuarioCod = Convert.ToInt32(r["codUsuario"]),
                            Nombre = r["nombre"].ToString(),
                            ApePaterno = r["apePaterno"].ToString(),
                            ApeMaterno = r["apeMaterno"].ToString(),
                            Rol = Convert.ToInt32(r["rol"]),
                        };
                    }
                }
            }
            return en;
        }
    }
}
