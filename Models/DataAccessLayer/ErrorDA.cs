using CineMajestic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
