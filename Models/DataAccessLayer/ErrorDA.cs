using CineMajestic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CineMajestic.Models.DataAccessLayer
{
    public class ErrorDA : DataAccess
    {
        public void UploadError(ErrorDTO error)
        {
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO [ERRORS] (NAME, DESCRIPTION, DATEADDED, STAFF_id, IMAGE) VALUES(@Name, @Description, @Dateadded, @Staff_id, @Image)";
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = error.Name;
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = error.Description;
                    DateTime date;
                    date = DateTime.ParseExact(error.DateAdded, "d/M/yyyy h:m:s tt", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None);
                    command.Parameters.Add("@Dateadded", SqlDbType.SmallDateTime).Value = date;
                    command.Parameters.Add("@Staff_id", SqlDbType.Int).Value = error.Staff_Id;
                    command.Parameters.Add("@Image", SqlDbType.NVarChar).Value = error.Image;
                    var rows = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                            error.Image = reader[8].ToString();
                            error.StatusColor = ConvertStatusToBrush(error.Status);
                            errors.Add(error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

        public void setEndDateAndCost(string id, DateTime endDate, string cost)
        {
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "UPDATE [ERRORS] SET STATUS=@status, ENDDATE=@enddate, COST=@cost WHERE id=@id";
                    command.Parameters.Add("@status", SqlDbType.NVarChar).Value = "Đã xử lý";
                    command.Parameters.Add("@cost", SqlDbType.Money).Value = cost;
                    //DateTime date;
                    //date = DateTime.ParseExact(error.DateAdded, "d/M/yyyy h:m:s tt", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None);
                    command.Parameters.Add("@enddate", SqlDbType.SmallDateTime).Value = endDate;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    var rows = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                throw ex;
            }
        }
    }
}
