using System;
using System.Collections.Generic;
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
            //_connectionString = "Server=tcp:192.168.0.199,1433;Database=CinemaManagement;User ID=sa;Password=Password.1";
            _connectionString = @"Data Source=DESKTOP-34OSP4G\SQLEXPRESS;Initial Catalog=QL_RapChieuPhim;Integrated Security=True";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
