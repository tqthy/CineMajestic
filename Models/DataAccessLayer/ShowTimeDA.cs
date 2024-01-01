using CineMajestic.Models.DTOs.ShowTimeManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace CineMajestic.Models.DataAccessLayer
{
    public class ShowTimeDA : DataAccess
    {
        //get danh sách show time theo phòng,tất cả
        public ObservableCollection<ShowTimeDTO> getDSShowTime(string Phong = "All")
        {
            ObservableCollection<ShowTimeDTO> list = new ObservableCollection<ShowTimeDTO>();
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    string truyvan = "";
                    if (Phong != "All")
                    {

                        truyvan =
                            "select Showtime.Id as ShowTimeId,ShowTime.StartTime,ShowTime.EndTime,ShowTime.PerSeatTicketPrice,Movie.id as MovieId,Movie.Length,Movie.Title,ImageSource,Auditorium.Name as phongchieu,ShowTime.Auditorium_Id as auditoriumId\n"
                            +
                            "from ShowTime\n"
                            +
                            "inner join Auditorium on ShowTime.Auditorium_Id=Auditorium.Id\n"
                            +
                            "inner join MOVIE on ShowTime.Movie_Id=MOVIE.id\n"
                            +
                            "where Auditorium.Name=N'" + Phong + "'";
                    }
                    else
                    {
                        truyvan =
                            truyvan =
                            "select Showtime.Id as ShowTimeId,ShowTime.StartTime,ShowTime.EndTime,ShowTime.PerSeatTicketPrice,Movie.id as MovieId,Movie.Length,Movie.Title,ImageSource,Auditorium.Name as phongchieu,ShowTime.Auditorium_Id as auditoriumId\n"
                            +
                            "from ShowTime\n"
                            +
                            "inner join Auditorium on ShowTime.Auditorium_Id=Auditorium.Id\n"
                            +
                            "inner join MOVIE on ShowTime.Movie_Id=MOVIE.id\n";
                    }

                    using (SqlCommand command = new SqlCommand(truyvan, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int showTimeId = reader.GetInt32(reader.GetOrdinal("ShowTimeId"));

                                DateTime startTime = reader.GetDateTime(reader.GetOrdinal("StartTime"));
                                string StartTime = startTime.ToString("dd/MM/yyyy HH:mm:ss");

                                DateTime endTime = reader.GetDateTime(reader.GetOrdinal("EndTime"));
                                string EndTime = endTime.ToString("dd/MM/yyyy HH:mm:ss");

                                int PerSeatTicketPrice = reader.GetInt32(reader.GetOrdinal("PerSeatTicketPrice"));

                                int MovieId = reader.GetInt32(reader.GetOrdinal("MovieId"));

                                string movieTitle = reader.GetString(reader.GetOrdinal("Title"));

                                int length = reader.GetInt32(reader.GetOrdinal("Length"));

                                string ImageSource = reader.GetString(reader.GetOrdinal("ImageSource"));
                                string phongchieu = reader.GetString(reader.GetOrdinal("phongchieu"));

                                int Auditorium_Id = reader.GetInt32(reader.GetOrdinal("auditoriumId"));

                                list.Add(new ShowTimeDTO(showTimeId, StartTime, EndTime, PerSeatTicketPrice, MovieId, movieTitle, length, ImageSource, phongchieu, Auditorium_Id));
                            }
                        }
                    }
                }
            }
            catch { }
            return list;
        }



        //thêm 1 showtime
        public void addShowtime(ShowTimeDTO showTime)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string insert =
                        "insert into ShowTime(StartTime,EndTime,PerSeatTicketPrice,Movie_Id,Auditorium_Id)\n"
                        +
                        "values("
                        +
                        "'" + showTime.StartTime + "',"
                        +
                        "'" + showTime.EndTime + "',"
                        +
                        showTime.PerSeatTicketPrice + ","
                        +
                        showTime.MovieId + ","
                        +
                        showTime.Auditorium_Id + ")";

                    using (SqlCommand command = new SqlCommand(insert, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }


        //xóa 1 showtime
        public void deleteShowtime(ShowTimeDTO showTimeDTO)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string delete = "delete Showtime where id=" + showTimeDTO.Id;
                    using (SqlCommand command = new SqlCommand(delete, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }


        //kiểm tra 1 showtime có đang hoạt động k(1 phim có đang được chiếu hay k)
        public bool checkShowtime(ShowTimeDTO showTimeDTO)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string select = "select StartTime, EndTime from ShowTime where Id=" + showTimeDTO.Id;
                    using (SqlCommand command = new SqlCommand(select, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime startTime = reader.GetDateTime(reader.GetOrdinal("StartTime"));
                                DateTime endTime = reader.GetDateTime(reader.GetOrdinal("EndTime"));
                                DateTime now = DateTime.Now;

                                if (now.CompareTo(startTime) > 0 && now.CompareTo(endTime) < 0)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            return false;
        }


        //kiểm tra 1 movie có đang  chiếu k(phục vụ bên movie)
        public bool checkShowtimeByMovie(int movieId)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string select = "select Movie_Id from ShowTime";
                    using (SqlCommand command = new SqlCommand(select, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int MovieId = reader.GetInt32(reader.GetOrdinal("Movie_Id"));

                                if (MovieId == movieId)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            } catch { }
            return false;
        }



        //kiểm tra khả năng có thể add 1 showtime
        public bool canAddShowtime(string phong,DateTime startTimeByADD)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string truyvan =
                        "select StartTime,EndTime\n"
                        +
                        "from Showtime\n"
                        +
                        "inner join Auditorium on ShowTime.Auditorium_Id=Auditorium.Id\n"
                        +
                        "where Auditorium.Name=" + "N'" + phong + "'";
                    using (SqlCommand command = new SqlCommand(truyvan, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime startTime = reader.GetDateTime(reader.GetOrdinal("StartTime"));
                                DateTime endTime = reader.GetDateTime(reader.GetOrdinal("EndTime"));

                                if (startTimeByADD.CompareTo(startTime) >= 0 && startTimeByADD.CompareTo(endTime) <= 0)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            return true;
        }
    }
}