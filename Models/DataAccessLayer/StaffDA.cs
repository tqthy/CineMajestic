using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineMajestic.Models.DTOs.StaffManagement;

namespace CineMajestic.Models.DataAccessLayer
{
    public class StaffDA:DataAccess
    {
        public ObservableCollection<StaffDTO> getDSNV()
        {
            ObservableCollection<StaffDTO>list = new ObservableCollection<StaffDTO>();
            using(SqlConnection connection=GetConnection())
            {
                connection.Open();
                string truyvan = "select * from Staff";
                using(SqlCommand command=new SqlCommand(truyvan, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("Id"));

                            string fullname = reader.GetString(reader.GetOrdinal("FullName"));

                            DateTime birthDate = reader.GetDateTime(reader.GetOrdinal("Birth"));
                            string birth = birthDate.ToString("dd/MM/yyyy");

                            string gender = reader.GetString(reader.GetOrdinal("Gender"));

                            string email = reader.GetString(reader.GetOrdinal("Email"));

                            string phoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));

                            int salary = reader.GetInt32(reader.GetOrdinal("Salary"));

                            string role = reader.GetString(reader.GetOrdinal("Role"));

                            DateTime NgayVL = reader.GetDateTime(reader.GetOrdinal("NgayVaolam"));
                            string ngayVL = birthDate.ToString("dd/MM/yyyy");

                            list.Add(new StaffDTO(id, fullname, birth, gender, email, phoneNumber, salary, role, ngayVL));
                        }
                    }
                }
            }
            return list;
        }


        //insert
        public void addStaff(StaffDTO staff)
        {
            using(SqlConnection connection = GetConnection())
            {
                connection.Open();
                string insert=
                    "insert into Staff\n"
                    +
                    "values("
                    +
                    "N'"+staff.FullName+"',"
                    +
                    "'"+staff.Birth+"',"
                    +
                    "'"+staff.Gender+"',"
                    +
                    "'"+staff.Email+"',"
                    +
                    "'"+staff.PhoneNumber+"',"
                    +
                    staff.Salary+","
                    +
                    "N'"+staff.Role+"',"
                    +
                    "'"+staff.NgayVaoLam+"')";

                using(SqlCommand command=new SqlCommand(insert, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
