﻿using CineMajestic.Models.DTOs.ShowTimeManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DataAccessLayer
{
    public class ShowTimeDA:DataAccess
    {
        //get danh sách show time theo phòng,tất cả
        public ObservableCollection<ShowTimeDTO> getDSShowTime(string Phong = "All")
        {
            ObservableCollection<ShowTimeDTO> list = new ObservableCollection<ShowTimeDTO>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string truyvan = "";
                if (Phong != "All")
                {

                    truyvan =
                        "select Showtime.Id as ShowTimeId,ShowTime.StartTime,ShowTime.EndTime,ShowTime.PerSeatTicketPrice,Movie.id as MovieId,Movie.Length,Movie.Title\n"
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
                        "select Showtime.Id as ShowTimeId,ShowTime.StartTime,ShowTime.EndTime,ShowTime.PerSeatTicketPrice,Movie.id as MovieId,Movie.Length,Movie.Title\n"
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

                            list.Add(new ShowTimeDTO(showTimeId, StartTime, EndTime, PerSeatTicketPrice, MovieId,movieTitle,length));
                        }
                    }
                }
            }

            return list;
        }
    }
}
