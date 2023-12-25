using CineMajestic.Models.DTOs;
using CineMajestic.ViewModels.MovieManagementVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DataAccessLayer
{
    public class MovieDA : DataAccess
    {
        //lấy danh sách phim
        public ObservableCollection<MovieDTO> getAllMovie()
        {
            ObservableCollection<MovieDTO> list = new ObservableCollection<MovieDTO>();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string truyvan = "select * from Movie";
                using (SqlCommand command = new SqlCommand(truyvan, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("id"));
                            string title = reader.GetString(reader.GetOrdinal("Title"));
                            string description = reader.GetString(reader.GetOrdinal("Description"));
                            string genre = reader.GetString(reader.GetOrdinal("Genre"));
                            string director = reader.GetString(reader.GetOrdinal("Director"));

                            DateTime releaseDate = reader.GetDateTime(reader.GetOrdinal("ReleaseDate"));
                            string ReleaseDate = releaseDate.ToString("dd/MM/yyyy");

                            string language = reader.GetString(reader.GetOrdinal("Language"));
                            string country = reader.GetString(reader.GetOrdinal("Country"));
                            int length = reader.GetInt32(reader.GetOrdinal("Length"));
                            string trailer = reader.GetString(reader.GetOrdinal("Trailer"));

                            DateTime startDate = reader.GetDateTime(reader.GetOrdinal("StartDate"));
                            string StartDate = releaseDate.ToString("dd/MM/yyyy");

                            string status = reader.GetString(reader.GetOrdinal("Status"));

                            string ImageSource = reader.GetString(reader.GetOrdinal("ImageSource"));
                            ImageSource = MotSoPTBoTro.pathProject() + @"Images\MovieManagement\" + ImageSource;

                            list.Add(new MovieDTO(id, title, description, director, ReleaseDate, language, country, length, trailer, StartDate, genre, status, ImageSource));
                        }
                    }
                }
            }
            return list;
        }
    }
}
