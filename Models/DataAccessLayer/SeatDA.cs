using CineMajestic.Models.DTOs.ShowTimeManagement;
using MaterialDesignThemes.Wpf.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DataAccessLayer
{
    public class SeatDA:DataAccess
    {
        public ObservableCollection<SeatDTO> getDSGhe(int auditoriumId)
        {
            AuditoriumDA auditoriumDA = new AuditoriumDA();
            ObservableCollection<SeatDTO> list = new ObservableCollection<SeatDTO>();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string truyvan = "select * from Seat where Auditorium_Id=" + auditoriumId;
                using (SqlCommand command = new SqlCommand(truyvan, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("Id"));
                            string location = reader.GetString(reader.GetOrdinal("Location"));
                            bool condition = reader.GetBoolean(reader.GetOrdinal("Condition"));
                            int auditorium_Id = reader.GetInt32(reader.GetOrdinal("Auditorium_Id"));

                            list.Add(new SeatDTO(id, location, condition, auditorium_Id));
                        }
                    }
                }
            }


            return list;
        }
    }
}
