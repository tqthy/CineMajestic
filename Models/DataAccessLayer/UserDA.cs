using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CineMajestic.Models.DTOs;

namespace CineMajestic.Models.DataAccessLayer
{
    public class UserDA : DataAccess
    {
        public void Add(UserDTO userModel)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [ACCOUNTS] WHERE username=@username AND [password]=@password";
                command.Parameters.Add("@username", SqlDbType.VarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.VarChar).Value = credential.Password;
                validUser = command.ExecuteScalar() == null ? false : true;
            }
            return validUser;
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
    }
}
