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
                    command.CommandText = "SELECT Total FROM [Bill] WHERE MONTH(BillDate)=@month AND YEAR(BillDate)=YEAR(GETDATE())";
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
        public long GetProductIncomeByMonth(string month)
        {
            long sumIncome = 0;
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT BD.Total FROM [BillDetail] BD JOIN [Bill] BT ON BD.Bill_Id=BT.Id " +
                        "WHERE MONTH(BillDate)=@month AND YEAR(BillDate)=YEAR(GETDATE())";
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

        public long GetIncomeByYear(string year)
        {
            long sumIncome = 0;
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT Total FROM [Bill] WHERE YEAR(BillDate)=@year";
                    command.Parameters.Add("@year", SqlDbType.Int).Value = year;
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

        public long GetProductIncomeByYear(string year)
        {
            long sumIncome = 0;
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT BD.Total FROM [BillDetail] BD JOIN [Bill] BT ON BD.Bill_Id=BT.Id " +
                        "WHERE YEAR(BillDate)=@year";
                    command.Parameters.Add("@year", SqlDbType.Int).Value = year;
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

        public List<Tuple<int, int>> GetCustomerDistribution()
        {
            List<Tuple<int, int>> result = new();

            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT DATEPART(HOUR, BillDate) DP, SUM(QuantityTicket) FROM Bill GROUP BY DATEPART(HOUR, BillDate) " +
                        "ORDER BY DP ASC";
                    using (var reader = command.ExecuteReader())
                    {
                        Tuple<int, int> item = new(0, 0);
                        while (reader.Read())
                        {
                            int item1 = Convert.ToInt32(reader[0].ToString());
                            int item2 = Convert.ToInt32(reader[1].ToString());
                            item = new(item1, item2);
                            result.Add(item);
                        }
                    }
                }
            }
            catch (Exception x)
            {
                throw x;
            }

            return result;
        }
    }
}
