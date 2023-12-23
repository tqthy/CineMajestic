using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CineMajestic.Models.DTOs;
using System.Collections;
using CineMajestic.Models.DTOs.StaffManagement;
using MS.WindowsAPICodePack.Internal;

namespace CineMajestic.Models.DataAccessLayer
{
    public class UserDA : DataAccess
    {
        public void Add(UserDTO userModel)
        {
            throw new NotImplementedException();
        }

        //public bool AuthenticateUser(NetworkCredential credential)
        //{
        //    bool validUser;
        //    using (var connection = GetConnection())
        //    using (var command = new SqlCommand())
        //    {
        //        connection.Open();
        //        command.Connection = connection;
        //        command.CommandText = "SELECT * FROM [ACCOUNTS] WHERE username=@username AND [password]=@password";
        //        command.Parameters.Add("@username", SqlDbType.VarChar).Value = credential.UserName;
        //        command.Parameters.Add("@password", SqlDbType.VarChar).Value = credential.Password;
        //        validUser = command.ExecuteScalar() == null ? false : true;
        //    }
        //    return validUser;
        //}



        //lấy dữ liệu bảng accounts
        public List<UserDTO> getAccounts()
        {
            List<UserDTO>list= new List<UserDTO>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string select = "select * from Accounts";
                using (SqlCommand command = new SqlCommand(select, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string Username = reader.GetString(reader.GetOrdinal("Username"));
                            string Password = reader.GetString(reader.GetOrdinal("Password"));
                            int Staff_Id  = reader.GetInt32(reader.GetOrdinal("Staff_Id"));
                            list.Add(new UserDTO(Username, Password, Staff_Id));
                        }
                    }
                }
            }

            return list;
        }

        public void Edit(UserDTO userModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDTO> GetByAll()
        {
            throw new NotImplementedException();
        }

        public UserDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public UserDTO GetByUsername(string username)
        {
            UserDTO? user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [ACCOUNTS] WHERE Username=@username";
                command.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserDTO()
                        {
                            //Id = reader[0].ToString(),
                            //Username = reader[1].ToString(),
                            //Password = string.Empty,
                            //AccountType = reader[3].ToString(),
                        };
                    }
                }
            }
            return user;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        //insert  account
        public void addAccount(UserDTO user)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string insert =
                    "insert into ACCOUNTS\n"
                    +
                    "values("
                    +
                    "'" + user.Username + "',"
                    +
                    "'" + user.Password + "',"
                    +
                    user.Staff_Id + ")";
                using (SqlCommand command = new SqlCommand(insert, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        //delete account
        public void deleteAccount(StaffDTO staff)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string delete =
                    "delete ACCOUNTS\n"
                +
                    "where Staff_Id=" + staff.Id;
                using (SqlCommand command = new SqlCommand(delete, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        //phương thức phục vụ phần quên pass(ở forgot) 
        public void changePassword(string username, string passwordNew)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string update =
                    "update Accounts\n"
                    +
                    "set Password=" + "'" + passwordNew + "'\n"
                    +
                    "where Username=" + "'" + username + "'";
                using (SqlCommand command = new SqlCommand(update, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }


        //phục vụ thông báo username hay mail k hợp lệ ở phần forgotpass
        public List<Tuple<string, string>> selectUserAndMail()
        {
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string select =
                    "select Username,Email\n"
                    +
                    "from Accounts\n"
                    +
                    "join Staff on Accounts.Staff_Id=Staff.Id";
                using (SqlCommand command = new SqlCommand(select, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string Username = reader.GetString(reader.GetOrdinal("Username"));
                            string Email = reader.GetString(reader.GetOrdinal("Email"));
                            result.Add(Tuple.Create(Username, Email));
                        }
                    }
                }
            }

            return result;
        }


        //truy vấn lấy danh sách username của bảng account
        public List<string> selectUsername()
        {
            List<string> result = new List<string>();
            using(SqlConnection connection = GetConnection())
            {
                connection.Open();
                string truyvan =
                    "select Username\n"
                    +
                    "from Accounts";
                using (SqlCommand command = new SqlCommand(truyvan, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string username = reader.GetString(reader.GetOrdinal("Username"));
                            result.Add(username);
                        }
                    }
                }
            }

            return result;
        }


        //phương thức phục vụ phần đổi pass ở Information
        public void changePassword(int Staff_Id,string passwordNew)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string update =
                    "update Accounts\n"
                    +
                    "set Password=" + "'" + passwordNew + "'\n"
                    +
                    "where Staff_Id=" + "'" + Staff_Id + "'";
                using (SqlCommand command = new SqlCommand(update, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }


        //truy vấn lấy password ứng với id là staffID(phục vụ phần changepassword ở information)
        public string passwordStaff_Id(int Staff_Id)
        {
            string result = ""; 
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string truyvan =
                    "select Password\n"
                    +
                    "from Accounts\n"
                    +
                    "where Staff_Id=" + Staff_Id;
                using (SqlCommand command = new SqlCommand(truyvan, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string password = reader.GetString(reader.GetOrdinal("Password"));
                            result = password;
                        }
                    }
                }
            }

            return result;
        }
    }
}
