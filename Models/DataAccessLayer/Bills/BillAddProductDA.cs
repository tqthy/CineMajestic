using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DataAccessLayer.Bills
{
    public class BillAddProductDA : DataAccess
    {
        public long GetOutcomeByMonth(string month)
        {
            long sumOutcome = 0;
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT Total FROM [Bill_AddProduct] WHERE MONTH(BillDate)=@month AND YEAR(BillDate)=YEAR(GETDATE())";
                    command.Parameters.Add("@month", SqlDbType.Int).Value = month;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sumOutcome += Convert.ToInt64(reader[0].ToString());
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sumOutcome;
        }

        public long GetOutcomeByYear(string year)
        {
            long sumOutcome = 0;
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT Total FROM [Bill_AddProduct] WHERE YEAR(BillDate)=@year";
                    command.Parameters.Add("@year", SqlDbType.Int).Value = year;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sumOutcome += Convert.ToInt64(reader[0].ToString());
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sumOutcome;
        }
    }
}
