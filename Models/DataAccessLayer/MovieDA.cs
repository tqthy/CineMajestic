using CineMajestic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Globalization;

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
                command.CommandText = "INSERT INTO [MOVIES] VALUES(@title, @description, @director, @releaseyear, @language, @country, @length, @trailer, @startdate, @enddate, @genreid, @poster)";
                command.Parameters.Add("@title", SqlDbType.NVarChar).Value = movie.Title;
                command.Parameters.Add("@description", SqlDbType.NVarChar).Value = movie.Description;
                command.Parameters.Add("@director", SqlDbType.NVarChar).Value = movie.Director;
                command.Parameters.Add("@releaseyear", SqlDbType.Int).Value = movie.ReleaseYear;
                command.Parameters.Add("@language", SqlDbType.NVarChar).Value = movie.Language;
                command.Parameters.Add("@country", SqlDbType.NVarChar).Value = movie.Country;
                command.Parameters.Add("@length", SqlDbType.Int).Value = movie.Length;
                command.Parameters.Add("@trailer", SqlDbType.NVarChar).Value = movie.Trailer;
                DateTime date;
                date = DateTime.ParseExact(movie.StartDate, "M/d/yyyy hh:mm:ss tt", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None);
                command.Parameters.Add("@startdate", SqlDbType.SmallDateTime).Value = date;
                date = DateTime.ParseExact(movie.EndDate, "M/d/yyyy hh:mm:ss tt", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None);
                command.Parameters.Add("@enddate", SqlDbType.SmallDateTime).Value = date;
                command.Parameters.Add("@genreid", SqlDbType.Int).Value = movie.Genre;
                command.Parameters.Add("@poster", SqlDbType.NVarChar).Value = movie.Poster;
                var rows_affected = command.ExecuteNonQuery();
            }
        }

        public void DeleteMovie(MovieDTO movie)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM [MOVIES] WHERE id = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = movie.Id;
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
                            Genre = reader[11].ToString(),
                            Poster = reader[12].ToString()
                        };
                        movies.Add(movie);
                    }
                }
            }
            return movies;
        }

        public void EditMovie(MovieDTO movie)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE [MOVIES]" +
                                        " SET Title = @title," +
                                        " Description = @description," +
                                        " Director = @director," +
                                        " ReleaseYear = @releaseyear," +
                                        " Language = @language," +
                                        " Country = @country," +
                                        " Length = @length," +
                                        " Trailer = @trailer," +
                                        " StartDate = @startdate," +
                                        " EndDate = @enddate," +
                                        " Genre_id = @genreid," +
                                        " Poster = @poster" +
                                        " WHERE id = @id";

                command.Parameters.Add("@id", SqlDbType.Int).Value = movie.Id;
                command.Parameters.Add("@title", SqlDbType.NVarChar).Value = movie.Title;
                command.Parameters.Add("@description", SqlDbType.NVarChar).Value = movie.Description;
                command.Parameters.Add("@director", SqlDbType.NVarChar).Value = movie.Director;
                command.Parameters.Add("@releaseyear", SqlDbType.Int).Value = movie.ReleaseYear;
                command.Parameters.Add("@language", SqlDbType.NVarChar).Value = movie.Language;
                command.Parameters.Add("@country", SqlDbType.NVarChar).Value = movie.Country;
                command.Parameters.Add("@length", SqlDbType.Int).Value = movie.Length;
                command.Parameters.Add("@trailer", SqlDbType.NVarChar).Value = movie.Trailer;
                DateTime date;
                date = DateTime.ParseExact(movie.StartDate, "M/d/yyyy hh:mm:ss tt", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None);
                command.Parameters.Add("@startdate", SqlDbType.SmallDateTime).Value = date;
                date = DateTime.ParseExact(movie.EndDate, "M/d/yyyy hh:mm:ss tt", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None);
                command.Parameters.Add("@enddate", SqlDbType.SmallDateTime).Value = date;
                command.Parameters.Add("@genreid", SqlDbType.Int).Value = movie.Genre;
                command.Parameters.Add("@poster", SqlDbType.NVarChar).Value = movie.Poster;
                var rows_affected = command.ExecuteNonQuery();
            }
        }
    }
}
