using CineMajestic.Models.DTOs;
using CineMajestic.ViewModels.MovieManagementVM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

                            int ReleaseYear = reader.GetInt32(reader.GetOrdinal("ReleaseYear"));

                            string language = reader.GetString(reader.GetOrdinal("Language"));
                            string country = reader.GetString(reader.GetOrdinal("Country"));
                            int length = reader.GetInt32(reader.GetOrdinal("Length"));
                            string trailer = reader.GetString(reader.GetOrdinal("Trailer"));

                            DateTime startDate = reader.GetDateTime(reader.GetOrdinal("StartDate"));
                            string StartDate = startDate.ToString("dd/MM/yyyy");

                            string status = reader.GetString(reader.GetOrdinal("Status"));

                            string ImageSource = reader.GetString(reader.GetOrdinal("ImageSource"));
                            ImageSource = MotSoPTBoTro.pathProject() + @"Images\MovieManagement\" + ImageSource;

                            int importPrice = reader.GetInt32(reader.GetOrdinal("ImportPrice"));

                            list.Add(new MovieDTO(id, title, description, director, ReleaseYear.ToString(), language, country, length, trailer, StartDate, genre, status, ImageSource, importPrice));
                        }
                    }
                }
            }
            return list;
        }


        //add 1 movie
        public void addMovie(MovieDTO movie)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string insert =
                    "insert into MOVIE(Title,description,genre,director,releaseYear,language,country,length,trailer,startDate,status,ImportPrice,imageSource)"
                    +
                    "values("
                    +
                    "N'" + movie.Title + "',"
                    +
                    "N'" + movie.Description + "',"
                    +
                    "N'" + movie.Genre + "',"
                    +
                    "N'" + movie.Director + "',"
                    +
                     int.Parse(movie.ReleaseYear) + ","
                    +
                    "N'" + movie.Language + "',"
                    +
                    "N'" + movie.Country + "',"
                    +
                    movie.Length + ","
                    +
                    "N'" + movie.Trailer + "',"
                    +
                    "'" + movie.StartDate + "',"
                    +
                    "N'" + movie.Status + "',"
                    +
                    movie.ImportPrice + ","
                    +
                    "'" + movie.ImageSource + "')";
                using (SqlCommand command = new SqlCommand(insert, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }



        //xóa 1 movie
        public void deleteMovie(MovieDTO movie)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string delete =
                    "delete Movie\n"
                    +
                    "where id=" + movie.Id;
                using (SqlCommand command = new SqlCommand(delete, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }


        //iden curent
        public int identCurrent()
        {
            int identCurrent;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string cm =
                    "select ident_current('Movie') as lastId";
                using (SqlCommand command = new SqlCommand(cm, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        identCurrent = (int)reader.GetDecimal(reader.GetOrdinal("lastId"));
                    }
                }
            }
            return identCurrent;
        }


        //lấy danh sách tên phim đang phát hành
        public List<Tuple<int, string>> getDSTitleDPH()
        {
            List<Tuple<int, string>> result = new List<Tuple<int, string>>();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string truyvan =
                    "select id,title\n"
                    +
                    "from movie\n"
                    +
                    "where Status=N'Đang phát hành'";


                using (SqlCommand command = new SqlCommand(truyvan, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("id"));
                            string title = reader.GetString(reader.GetOrdinal("Title"));

                            result.Add(new Tuple<int, string>(id, title));
                        }
                    }
                }
            }
            return result;

        }


        //lấy thời lượng phim 
        public int MovieLength(int id)
        {
            int kq = 0;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string truyvan = "select length from movie\n"
                    + "where Id=" + id;
                using (SqlCommand command = new SqlCommand(truyvan, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        kq = reader.GetInt32(reader.GetOrdinal("Length"));
                    }
                }
            }

            return kq;
        }


        //update(edit) movie
        public void editMovie(MovieDTO movie)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string update =
                    "update Movie\n"
                    +
                    "set Title=" + "N'" + movie.Title + "',"
                    +
                    "Description=" + "N'" + movie.Description + "',"
                    +
                    "Genre=" + "N'" + movie.Genre + "',"
                    +
                    "Director=" + "N'" + movie.Director + "',"
                    +
                    "ReleaseYear=" + movie.ReleaseYear + ","
                    +
                    "Language=" + "N'" + movie.Language + "',"
                    +
                    "Country=" + "N'" + movie.Country + "',"
                    +
                    "Length=" + movie.Length + ","
                    +
                    "Trailer=" + "N'" + movie.Trailer + "',"
                    +
                    "StartDate=" + "'" + movie.StartDate + "',"
                    +
                    "Status=" + "N'" + movie.Status + "',"
                    +
                    "ImportPrice=" + movie.ImportPrice + ","
                    +
                    "ImageSource=" + "'" + movie.ImageSource + "'\n"
                    +
                    "where Id=" + movie.Id;
                using (SqlCommand command = new SqlCommand(update, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }


        //lấy toàn bộ imageSource phục vụ việc xóa ảnh
        public List<string> listImageSource()
        {
            List<string> list = new List<string>();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string truyvan =
                    "select ImageSource from Movie";
                using (SqlCommand command = new SqlCommand(truyvan, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string imageSource = reader.GetString(reader.GetOrdinal("ImageSource"));
                            list.Add(imageSource);
                        }
                    }
                }
            }


            return list;
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
            List<MovieStatisticsDTO> results = new List<MovieStatisticsDTO>();
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
