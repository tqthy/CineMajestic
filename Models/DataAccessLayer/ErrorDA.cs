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
    }
}
