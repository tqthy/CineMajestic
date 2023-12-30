using CineMajestic.Models.DTOs.ShowTimeManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DataAccessLayer
{
    public class SeatForShowTimeDA : DataAccess
    {
        //lấy danh sách seat theo showtime
        public ObservableCollection<SeatForShowTimeDTO> getDSGhe(int showtime_Id)
        {
            ObservableCollection<SeatForShowTimeDTO> list = new ObservableCollection<SeatForShowTimeDTO>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string truyvan =
                    "select SeatForShowtime.Id as SeatForShowTime_Id,SeatForShowtime.Seat_Id as Seat_Id,ShowTime_Id,Location,Condition\n"
                    +
                    "from SeatForShowtime\n"
                    +
                    "inner join Seat on Seat.Id=SeatForShowtime.Id\n"
                    +
                    "where SeatForShowtime.ShowTime_Id=" + showtime_Id;
                using (SqlCommand command = new SqlCommand(truyvan, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int seatForShowtimeId= reader.GetInt32(reader.GetOrdinal("SeatForShowTime_Id"));
                            int seatId= reader.GetInt32(reader.GetOrdinal("Seat_Id"));
                            int showtimeId= reader.GetInt32(reader.GetOrdinal("ShowTime_Id"));
                            string location = reader.GetString(reader.GetOrdinal("Location"));
                            bool condition = reader.GetBoolean(reader.GetOrdinal("Condition"));

                            list.Add(new SeatForShowTimeDTO(seatForShowtimeId, seatId, showtime_Id, location, condition));
                        }
                    }
                }
            }


            return list;
        }




        //chuyển trạng thái ghế là được chọn ứng với showtime đc chọn 
        public void choiceSeat(SeatForShowTimeDTO seat)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string update =
                    "update SeatForShowtime\n"
                    +
                    "set Condition=1\n"
                    +
                    "where Id=" + seat.Id;
                using (SqlCommand command = new SqlCommand(update, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
