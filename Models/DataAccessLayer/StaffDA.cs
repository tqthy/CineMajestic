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
        private static int soLuongChuSo;
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
                        soLuongChuSo = identCurrent().ToString().Length;//phục vụ format id

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
                    "N'"+staff.Gender+"',"
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


        //xóa 1 staff
        public void deleteStaff(StaffDTO staff)
        {
            using(SqlConnection connection = GetConnection())
            {
                connection.Open();
                string delete =
                    "delete Staff\n"
                    +
                    "where Id=" + staff.Id;
                using (SqlCommand command = new SqlCommand(delete, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }


        //update
        public void updateStaff(StaffDTO staff)
        {
            using(SqlConnection connection = GetConnection())
            {
                connection.Open();
                string update =
                    "update Staff\n"
                    +
                    "set FullName=" + "N'" + staff.FullName + "',"
                    +
                    "Birth=" + "'" + staff.Birth + "',"
                    +
                    "Gender=" + "N'" + staff.Gender + "',"
                    +
                    "Email=" + "'" + staff.Email + "',"
                    +
                    "PhoneNumber=" + "'" + staff.PhoneNumber + "',"
                    +
                    "Salary=" + staff.Salary + ","
                    +
                    "Role=" + "N'" + staff.Role + "',"
                    +
                    "NgayVaolam=" + "'" + staff.NgayVaoLam + "'\n"
                    +
                    "where Id=" + staff.Id;


                using (SqlCommand command = new SqlCommand(update, connection))
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
                    "select ident_current('Staff') as lastId";
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

        public static string formatID(int id, string type = "NV")
        {
            string format = "";
            if (soLuongChuSo < 4)
            {
                format = "000";
            }
            else
            {
                for (int k = 0; k < soLuongChuSo; k++)
                {
                    format += "0";
                }
            }
            string ID = id.ToString();

            int i = format.Length - 1;
            int j = ID.Length - 1;
            char[] s1 = format.ToCharArray();
            char[] s2 = ID.ToCharArray();
            while (i >= 0 && j >= 0)
            {
                s1[i--] = s2[j--];
            }

            format = new string(s1);
            format = type + format;
            return format;
        }
    }
}
