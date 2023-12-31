using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using CineMajestic.Models.DTOs;
using CineMajestic.Models.DTOs.StaffManagement;
using CineMajestic.ViewModels.InformationManagement;
using Microsoft.Extensions.Primitives;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CineMajestic.Models.DataAccessLayer
{
    public class StaffDA : DataAccess
    {
        private static int soLuongChuSo;
        public ObservableCollection<StaffDTO> getDSNV()
        {
            ObservableCollection<StaffDTO> list = new ObservableCollection<StaffDTO>();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string truyvan = "select * from Staff";
                using (SqlCommand command = new SqlCommand(truyvan, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        soLuongChuSo = identCurrent().ToString().Length;//phục vụ format id,này là khi vào quản lý nv

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
                            string ngayVL = NgayVL.ToString("dd/MM/yyyy");

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
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string insert =
                    "insert into Staff(FullName,Birth,Gender,Email,PhoneNumber,Salary,Role,NgayVaolam)\n"
                    +
                    "values("
                    +
                    "N'" + staff.FullName + "',"
                    +
                    "'" + staff.Birth + "',"
                    +
                    "N'" + staff.Gender + "',"
                    +
                    "'" + staff.Email + "',"
                    +
                    "'" + staff.PhoneNumber + "',"
                    +
                    staff.Salary + ","
                    +
                    "N'" + staff.Role + "',"
                    +
                    "'" + staff.NgayVaoLam + "')";

                using (SqlCommand command = new SqlCommand(insert, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }


        //xóa 1 staff
        public void deleteStaff(StaffDTO staff)
        {
            using (SqlConnection connection = GetConnection())
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
            using (SqlConnection connection = GetConnection())
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



        //lấy nhân viên có id là staff_id
        public StaffDTO Staffstaff_Id(int Staff_Id)
        {
            StaffDTO staffDTO = null;

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string truyvan =
                    "select * from Staff\n"
                    +
                    "where Id=" + Staff_Id;

                using (SqlCommand command = new SqlCommand(truyvan, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        soLuongChuSo = identCurrent().ToString().Length;//phục vụ format id,này là khi vào cá nhân

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
                            string ngayVL = NgayVL.ToString("dd/MM/yyyy");

                            string imageSource = reader.GetString(reader.GetOrdinal("ImageSource"));

                            staffDTO = new StaffDTO(id, fullname, birth, gender, email, phoneNumber, salary, role, ngayVL, MotSoPTBoTro.pathProject() + @"Images\StaffManagement\" + imageSource);
                        }
                    }
                }
            }
            return staffDTO;
        }



        //update image
        public void updateImageStaff(int id, string imageSource)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string update =
                    "update Staff\n"
                    +
                    "set ImageSource=" + "'" + imageSource + "'\n"

                    +
                    "where Id=" + id;


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
                    "select ImageSource from Staff";
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

        public long GetSalaryByYear(string year)
        {
            long res = 0;
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT SUM(Total) FROM [Staff_Salary] WHERE YEAR(BillDate)=@year";
                    command.Parameters.Add("@year", SqlDbType.Int).Value = year;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!string.IsNullOrEmpty(reader[0].ToString())) res += Convert.ToInt64(reader[0].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }

        public long GetSalaryByMonth(string month)
        {
            long res = 0;
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT SUM(Total) FROM [Staff_Salary] WHERE YEAR(BillDate)=YEAR(GETDATE()) AND MONTH(BillDate)=@month";
                    command.Parameters.Add("@month", SqlDbType.Int).Value = month;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!string.IsNullOrEmpty(reader[0].ToString())) res += Convert.ToInt64(reader[0].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }
    }
}
