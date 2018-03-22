using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parte02.AD
{

    public interface IDataContext
    {
        MySqlConnection GetConnection();
    }

    public class ADContext:IDataContext
    {
        public string ConnectionString { get; set; }
        
        public ADContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
