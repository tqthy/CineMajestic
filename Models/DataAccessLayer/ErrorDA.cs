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
                    command.CommandText = "INSERT INTO [ERROR](NAME, DESCRIPTION, DATEADDED, STAFF_id) VALUES(@Name, @Description, @Dateadded, @Staff_id)";
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = error.Name;
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = error.Description;
                    DateTime date;
                    date = DateTime.ParseExact(error.DateAdded, "M/d/yyyy hh:mm:ss tt", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None);
                    command.Parameters.Add("@Dateadded", SqlDbType.SmallDateTime).Value = date;
                    command.Parameters.Add("@Staff_id", SqlDbType.Int).Value = error.Staff_Id;
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
