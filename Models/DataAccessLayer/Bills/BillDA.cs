using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineMajestic.Models.DTOs.Bills;

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
                    command.CommandText = "SELECT DATEPART(HOUR, StartTime) DP, SUM(QuantityTicket) FROM Bill JOIN ShowTime ON Bill.ShowTime_Id=ShowTime.Id GROUP BY DATEPART(HOUR, StartTime) " +
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



        //thêm 1 bill
        public void addBill(BillDTO billDTO)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string insert =
                        "insert into Bill(Staff_Id,Customer_Id,ShowTime_Id,QuantityTicket,PerSeatTicketPrice,Discount,Note,Total,BillDate)\n"
                        +
                        "values("
                        +
                        billDTO.Staff_Id + ","
                        +
                        billDTO.Customer_Id + ","
                        +
                        billDTO.Showtime_Id + ","
                        +
                        billDTO.QuantityTicket + ","
                        +
                        billDTO.PerTicketPrice + ","
                        +
                        billDTO.Discount + ","
                        +
                        "N'" + billDTO.Note + "',"
                        +
                        billDTO.Total + ","
                        +
                        "'" + billDTO.BillDate + "')";
                    using (SqlCommand command = new SqlCommand(insert, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }



        //iden curent
        public int identCurrent()
        {
            int identCurrent = 0; 
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string cm =
                        "select ident_current('Bill') as lastId";
                    using (SqlCommand command = new SqlCommand(cm, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            identCurrent = (int)reader.GetDecimal(reader.GetOrdinal("lastId"));
                        }
                    }
                }
            }
            catch { }

            return identCurrent;
        }
    }
}
