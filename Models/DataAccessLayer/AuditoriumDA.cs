using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CineMajestic.Models.DataAccessLayer
{
    public class AuditoriumDA:DataAccess
    {
        //lấy danh sách phòng
        public List<Tuple<int, string>> getDSPhong()
        {
            List<Tuple<int, string>> result = new List<Tuple<int, string>>();
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string truyvan =
                        "select id,Name\n"
                        +
                        "from Auditorium";


                    using (SqlCommand command = new SqlCommand(truyvan, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal("id"));
                                string name = reader.GetString(reader.GetOrdinal("Name"));

                                result.Add(new Tuple<int, string>(id, name));
                            }
                        }
                    }
                }
            } 
            catch { }
            return result;

        }


        //lấy id phòng theo tên phòng
        public int AuditoriumId(string phong)
        {
            int kq = 0;
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string truyvan =
                        "select id from Auditorium\n"
                        +
                        "where Name=N'" + phong + "'";
                    using (SqlCommand command = new SqlCommand(truyvan, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                kq = reader.GetInt32(reader.GetOrdinal("id"));
                            }
                        }
                    }
                }
            }
            catch { }
            return kq;
        }
    }
}
