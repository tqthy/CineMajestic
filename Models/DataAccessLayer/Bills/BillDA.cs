using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DataAccessLayer.Bills
{
    public class BillDA : DataAccess
    {
        public long GetIncomeByMonth(string month)
        {
            long sumIncome = 0;
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT Total FROM [Billtest] WHERE MONTH(BillDate)=@month AND YEAR(BillDate)=YEAR(GETDATE())";
                    command.Parameters.Add("@month", SqlDbType.Int).Value = month;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sumIncome += Convert.ToInt64(reader[0].ToString());
                        }

                    }
                }

            } 
            catch (Exception ex)
            {
                throw ex;
            }
            return sumIncome;
        }
    }
}
