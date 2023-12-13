using CineMajestic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace CineMajestic.Models.DataAccessLayer
{
    public class MovieDA : DataAccess
    {
        public void AddMovie(MovieDTO movie)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO [MOVIES](Title, Country, Length, Director) VALUES(@title, @country, @length, @director)";
                command.Parameters.Add("@title", SqlDbType.NVarChar).Value = movie.Title;
                command.Parameters.Add("@country", SqlDbType.NVarChar).Value = movie.Country;
                command.Parameters.Add("@director", SqlDbType.NVarChar).Value = movie.Director;
                command.Parameters.Add("@length", SqlDbType.Int).Value = movie.Length;
                var rows_affected = command.ExecuteNonQuery();
            }
        }
        public List<MovieDTO> GetAllMovies()
        {
            List<MovieDTO> movies = new List<MovieDTO>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [MOVIES]";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MovieDTO movie = new MovieDTO()
                        {
                            Id = reader[0].ToString(),
                            Title = reader[1].ToString(),
                            Description = reader[2].ToString(),
                            Director = reader[3].ToString(),
                            ReleaseYear = reader[4].ToString(),
                            Language = reader[5].ToString(),
                            Country = reader[6].ToString(),
                            Length = reader[7].ToString(),
                            Trailer = reader[8].ToString(),
                            StartDate = reader[9].ToString(),
                            EndDate = reader[10].ToString(),
                            Genre = "Funny"
                        };
                        movies.Add(movie);
                    }
                }
            }
            return movies;
        } 
    }
}
