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
           // _connectionString = "Server=tcp:cinemajestic.database.windows.net,1433;Initial Catalog=Cinemajestic;Persist Security Info=False;User ID=cinemajestic;Password=Binh@2k4;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            _connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;

        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
