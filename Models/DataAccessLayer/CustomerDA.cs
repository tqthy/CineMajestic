using CineMajestic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DataAccessLayer
{
    public class CustomerDA : DataAccess
    {
        private static int soLuongChuSo;

        //lấy data từ bảng Voucher
        public ObservableCollection<CustomerDTO> getDSCustomer()
        {
            soLuongChuSo = identCurrent().ToString().Length;//phục vụ format id

            ObservableCollection<CustomerDTO> list = new ObservableCollection<CustomerDTO>();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string truyvan = "select * from Customer";
                using (SqlCommand command = new SqlCommand(truyvan, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("ID"));

                            string fullname = reader.GetString(reader.GetOrdinal("FULLNAME"));

                            string phonenumber = reader.GetString(reader.GetOrdinal("PHONENUMBER"));

                            string email = reader.GetString(reader.GetOrdinal("EMAIL"));

                            int point = reader.GetInt32(reader.GetOrdinal("POINT"));

                            DateTime birth = reader.GetDateTime(reader.GetOrdinal("BIRTH"));
                            string Birth = birth.ToString("dd/MM/yyyy");

                            DateTime regdate = reader.GetDateTime(reader.GetOrdinal("REGDATE"));
                            string Regdate = regdate.ToString("dd/MM/yyyy");

                            string gender = reader.GetString(reader.GetOrdinal("GENDER"));

                            list.Add(new CustomerDTO(id,fullname,phonenumber,email,point,Birth,Regdate,gender));
                        }
                    }
                }
            }
            return list;
        }
        
        //edit 1 khách hàng
        public void editCustomer(CustomerDTO customer)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string update =
                    "update Customer\n"
                    +
                    "set FullName=" + "N'" + customer.FullName + "',"
                    +
                    "PhoneNumber =" + "'" + customer.PhoneNumber+ "',"
                    +
                    "Email=" + "'" + customer.Email+ "',"
                    +
                    "RegDate=" + "'" + customer.RegDate + "',"
                    +
                    "Point=" + customer.Point + "\n"
                    +
                    "where Id=" + customer.Id;

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
                    "select ident_current('CUSTOMER') as lastId";
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

        public static string formatID(int id, string type = "KH")
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
