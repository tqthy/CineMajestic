using CineMajestic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DataAccessLayer
{
    public class ShowTimeDA : DataAccess
    {
        public List<ShowTimeDTO> GetAllShowTimeByDate(DateTime date)
        {
            
            List<ShowTimeDTO> result = new List<ShowTimeDTO>();
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.CommandText = "SELECT * FROM [SCREENING] WHERE YEAR(StartTime)=@year AND MONTH(StartTime)=@month AND DAY(StartTime)=@day";
                    command.Parameters.Add("@year", SqlDbType.Int).Value = date.Year;
                    command.Parameters.Add("@month", SqlDbType.Int).Value = date.Month;
                    command.Parameters.Add("@day", SqlDbType.Int).Value = date.Day;
                    using (var reader = command.ExecuteReader())
                    {
                        List<(string, string, DateTime)> lines = new();
                        while (reader.Read())
                        {
                            (string, string, DateTime) t = (reader[0].ToString(), reader[1].ToString(), Convert.ToDateTime(reader[2]));
                            lines.Add(t);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
