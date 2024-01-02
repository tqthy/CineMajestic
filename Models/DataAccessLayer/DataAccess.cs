using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DataAccessLayer
{
    public abstract class DataAccess
    {
        private readonly string _connectionString;
        public DataAccess()
        {
              _connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
