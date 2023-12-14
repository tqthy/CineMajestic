using CineMajestic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DataAccessLayer
{
    public class GenreDA : DataAccess
    {
        public List<GenreDTO> GetAllGenres()
        {
            List<GenreDTO> genres = new List<GenreDTO>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [GENRES]";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GenreDTO genre = new GenreDTO()
                        {
                            Id = reader[0].ToString(),
                            Title = reader[1].ToString(),
                        };
                        genres.Add(genre);
                    }
                }
            }
            return genres;
        }

    }
}
