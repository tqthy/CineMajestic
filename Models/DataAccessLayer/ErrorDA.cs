using CineMajestic.Models.DTOs;
using CineMajestic.ViewModels;
using HarfBuzzSharp;
using LiveChartsCore.Kernel;
using OfficeOpenXml.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CineMajestic.Models.DataAccessLayer
{
    public class ErrorDA : DataAccess
    {
        public void UploadError(ErrorDTO error)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string insert = @"
                    INSERT INTO ERRORS (NAME, DESCRIPTION, DATEADDED, STATUS, STAFF_id, ImageSource)
                    VALUES (@Name, @Description, @DateAdded, @Status, @StaffId, @ImageSource)";

                    using (SqlCommand command = new SqlCommand(insert, connection))
                    {
                        byte[] imageBytes = ImageVsSQL.BitmapImageToByteArray(error.Image); 

                        command.Parameters.AddWithValue("@Name", error.Name);
                        command.Parameters.AddWithValue("@Description", error.Description);
                        command.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                        command.Parameters.AddWithValue("@Status", error.Status);
                        command.Parameters.AddWithValue("@StaffId", error.Staff_Id);
                        command.Parameters.AddWithValue("@ImageSource", imageBytes);

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch { }

        }

        public List<ErrorDTO> GetAllErrors()
        {
            List<ErrorDTO> errors = new List<ErrorDTO>();
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM [ERRORS]";
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            ErrorDTO error = new ErrorDTO();
                            error.Id = reader[0].ToString();
                            error.Name = reader[1].ToString();
                            error.Description = reader[2].ToString();
                            if (reader[3] != null) error.DateAdded = reader[3].ToString();
                            error.Status = reader[4].ToString();
                            if (reader[5] != null) error.EndDate = reader[5].ToString();
                            if (reader[6] != null) error.Cost = reader[6].ToString();
                            error.Staff_Id = reader[7].ToString();

                            byte[] imageBytes = (byte[])reader["ImageSource"];
                            BitmapImage imageSource = ImageVsSQL.ByteArrayToBitmapImage(imageBytes);
                            error.Image = imageSource;

                            error.StatusColor = ConvertStatusToBrush(error.Status);
                            errors.Add(error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }

            return errors;
        }

        public System.Windows.Media.Brush ConvertStatusToBrush(string status)
        {
            if (status == "Đang xử lý") return new SolidColorBrush(System.Windows.Media.Colors.DarkOrange);
            if (status == "Đã xử lý") return new SolidColorBrush(System.Windows.Media.Colors.DarkGreen);
            if (status == "Đã huỷ") return new SolidColorBrush(System.Windows.Media.Colors.DarkGray);
            return new SolidColorBrush(System.Windows.Media.Colors.DarkRed);
        }

        //public void setEndDateAndCost(string id, DateTime endDate, string cost)
        //{
        //  //  try
        //    {
        //        using (var connection = GetConnection())
        //        using (var command = new SqlCommand())
        //        {
        //            connection.Open();
        //            command.Connection = connection;
        //            command.CommandText = "UPDATE [ERRORS] SET STATUS=@status, ENDDATE=@enddate, COST=@cost WHERE id=@id";
        //            command.Parameters.Add("@status", SqlDbType.NVarChar).Value = "Đã xử lý";
        //            command.Parameters.Add("@cost", SqlDbType.Int).Value = cost;
        //            command.Parameters.Add("@enddate", SqlDbType.SmallDateTime).Value = endDate;
        //            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
        //            var rows = command.ExecuteNonQuery();
        //        }
        //    }
        //  //  catch (Exception ex)
        //    {
        //       // throw ex;
        //    }
        //}
        public void setEndDateAndCost(string id, DateTime endDate, string cost)
        {
            try
            {
                int costValue = int.Parse(cost);
                int idValue = int.Parse(id);

                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "UPDATE [ERRORS] SET STATUS=@status, ENDDATE=@enddate, COST=@cost WHERE ID=@id";
                    command.Parameters.AddWithValue("@status", "Đã xử lý");
                    command.Parameters.AddWithValue("@cost", costValue);
                    command.Parameters.AddWithValue("@enddate", endDate);
                    command.Parameters.AddWithValue("@id", idValue);
                    var rows = command.ExecuteNonQuery();
                }
            }
            catch { }
        }


        public void setStatus(string iD, int comboBoxStatusIndex)
        {
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "UPDATE [ERRORS] SET STATUS=@status, COST=0, ENDDATE=GETDATE() WHERE id=@id";
                    string status = "Chờ tiếp nhận";
                    if (comboBoxStatusIndex == 1) status = "Đang xử lý";
                    if (comboBoxStatusIndex == 3) status = "Đã huỷ";
                    command.Parameters.Add("@status", SqlDbType.NVarChar).Value = status;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = iD;
                    var rows = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        public long GetCostByMonth(string month)
        {
            long result = 0;
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT COST FROM [ERRORS] WHERE MONTH(ENDDATE)=@month AND YEAR(ENDDATE)=YEAR(GETDATE()) AND COST IS NOT NULL";
                    command.Parameters.Add("@month", SqlDbType.Int).Value = month;
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            result += Convert.ToInt64(reader[0].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return result;
        }

        public long GetCostByYear(string year)
        {
            long result = 0;
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT COST FROM [ERRORS] WHERE YEAR(ENDDATE)=@year AND COST IS NOT NULL";
                    command.Parameters.Add("@year", SqlDbType.Int).Value = year;
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            result += Convert.ToInt64(reader[0].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
              //  throw ex;
            }
            return result;
        }



    }
}
