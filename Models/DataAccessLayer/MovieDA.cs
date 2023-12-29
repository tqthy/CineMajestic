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
using System.Diagnostics.Metrics;
using System.IO;
using System.Reflection;

namespace CineMajestic.Models.DataAccessLayer
{
    public class MovieDA : DataAccess
    {
        public void AddMovie(MovieDTO movie)
        {
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MovieDTO GetMovieById(string movie_id)
        {
            MovieDTO movie = new MovieDTO();
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM [MOVIES] WHERE id=@movie_id";
                    command.Parameters.Add("@movie_id", SqlDbType.Int).Value = movie_id;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            movie.Id = reader[0].ToString();
                            movie.Title = reader[1].ToString();
                            movie.Description = reader[2].ToString();
                            movie.Director = reader[3].ToString();
                            movie.ReleaseYear = reader[4].ToString();
                            movie.Language = reader[5].ToString();
                            movie.Country = reader[6].ToString();
                            movie.Length = reader[7].ToString();
                            movie.Trailer = reader[8].ToString();
                            movie.StartDate = reader[9].ToString();
                            movie.EndDate = reader[10].ToString();
                            movie.Genre = reader[11].ToString();
                            movie.Poster = reader[12].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return movie;
        }

        public void DeleteMovie(MovieDTO movie)
        {
            try
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
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<MovieDTO> GetAllMovies()
        {
            List<MovieDTO> movies = new List<MovieDTO>();
            try
            {
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return movies;
        }

        public void EditMovie(MovieDTO movie)
        {
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public long GetCostByYear(string year)
        {
            long cost = 0;
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT Total FROM [Bill_AddMovie] WHERE YEAR(BillDate)=@year";
                    command.Parameters.Add("@year", SqlDbType.Int).Value = year;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cost += Convert.ToInt64(reader[0].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cost;
        }

        public long GetCostByMonth(string month)
        {
            long cost = 0;
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT Total FROM [Bill_AddMovie] WHERE YEAR(BillDate)=YEAR(GETDATE()) AND MONTH(BillDate)=@month";
                    command.Parameters.Add("@month", SqlDbType.Int).Value = month;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cost += Convert.ToInt64(reader[0].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cost;
        }

        public List<MovieStatisticsDTO> GetTopMovieByMonth(string month)
        {
            List<MovieStatisticsDTO > results = new List<MovieStatisticsDTO>();
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT Title, Sum(QuantityTicket), SUM(Bill.PerSeatTicketPrice*QuantityTicket) AS DoanhThu FROM Bill JOIN ShowTime ON Bill.ShowTime_Id=ShowTime.Id " +
                        "JOIN MOVIE ON ShowTime.Movie_Id = MOVIE.id WHERE MONTH(BillDate)=@month AND YEAR(BillDate)=YEAR(GETDATE()) GROUP BY MOVIE.id, Title ORDER BY DoanhThu DESC";
                    command.Parameters.Add("@month", SqlDbType.Int).Value = month;
                    using (var reader = command.ExecuteReader())
                    {
                        int rank = 0;
                        while (reader.Read())
                        {
                            MovieStatisticsDTO movie = new MovieStatisticsDTO()
                            {
                                Rank = (++rank).ToString(),
                                Title = reader[0].ToString(),
                                ViewCount = reader[1].ToString(),
                                Income = Convert.ToInt64(reader[2].ToString()).ToString("N0")
                            };
                            results.Add(movie);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return results;
        }

        public List<MovieStatisticsDTO> GetTopMovieByYear(string year)
        {
            List<MovieStatisticsDTO> results = new List<MovieStatisticsDTO>();
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT Title, Sum(QuantityTicket), SUM(Bill.PerSeatTicketPrice*QuantityTicket) AS DoanhThu FROM Bill JOIN ShowTime ON Bill.ShowTime_Id=ShowTime.Id " +
                        "JOIN MOVIE ON ShowTime.Movie_Id = MOVIE.id WHERE YEAR(BillDate)=@year GROUP BY MOVIE.id, Title ORDER BY DoanhThu DESC";
                    command.Parameters.Add("@year", SqlDbType.Int).Value = year;
                    using (var reader = command.ExecuteReader())
                    {
                        int rank = 0;
                        while (reader.Read())
                        {
                            MovieStatisticsDTO movie = new MovieStatisticsDTO()
                            {
                                Rank = (++rank).ToString(),
                                Title = reader[0].ToString(),
                                ViewCount = reader[1].ToString(),
                                Income = Convert.ToInt64(reader[2].ToString()).ToString("N0")
                            };
                            results.Add(movie);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return results;
        }
    }
}