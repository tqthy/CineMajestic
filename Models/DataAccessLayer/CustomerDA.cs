using CineMajestic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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



        //lấy khách hàng vip 
        //400đ vip 1, 800đ vip2, 1200đ vip3
        public  List<CustomerDTO>getKHVip(string LoaiVoucher)
        {
            int pointMin = 0;
            if(LoaiVoucher=="Vip 1")
            {
                pointMin = 400;
            }
            else if(LoaiVoucher=="Vip 2")
            {
                pointMin = 800;
            }
            else
            {
                pointMin = 1200;
            }
            List<CustomerDTO> list=new List<CustomerDTO> ();
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
                            int point = reader.GetInt32(reader.GetOrdinal("POINT"));

                            if (point >= pointMin)
                            {

                                int id = reader.GetInt32(reader.GetOrdinal("ID"));

                                string fullname = reader.GetString(reader.GetOrdinal("FULLNAME"));

                                string phonenumber = reader.GetString(reader.GetOrdinal("PHONENUMBER"));

                                string email = reader.GetString(reader.GetOrdinal("EMAIL"));

                                DateTime birth = reader.GetDateTime(reader.GetOrdinal("BIRTH"));
                                string Birth = birth.ToString("dd/MM/yyyy");

                                DateTime regdate = reader.GetDateTime(reader.GetOrdinal("REGDATE"));
                                string Regdate = regdate.ToString("dd/MM/yyyy");

                                string gender = reader.GetString(reader.GetOrdinal("GENDER"));

                                list.Add(new CustomerDTO(id, fullname, phonenumber, email, point, Birth, Regdate, gender));
                            }
                        }
                    }
                }
            }
            return list;
        }


        //cập nhật lại điểm của khách hàng
        public void updatePoint(CustomerDTO customerDTO,string LoaiVoucher)
        {
            int pointMin = 0;
            if (LoaiVoucher == "Vip 1")
            {
                pointMin = 400;
            }
            else if (LoaiVoucher == "Vip 2")
            {
                pointMin = 800;
            }
            else
            {
                pointMin = 1200;
            }
            using (SqlConnection connection = GetConnection())
            {
                int pointNew = customerDTO.Point - pointMin;
                connection.Open();
                string update =
                    "update Customer\n"
                    +
                    "set Point=" + pointNew
                    +
                    "where Id=" + customerDTO.Id;

                using (SqlCommand command = new SqlCommand(update, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

        }

        public List<CustomerStatisticsDTO> GetTopCustomerByMonth(string month)
        {
            List<CustomerStatisticsDTO> result = new List<CustomerStatisticsDTO>();
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT FullName, PhoneNumber, SUM(Total) AS ChiTieu FROM CUSTOMER" +
                        " KH JOIN Bill BT ON KH.Id = BT.Customer_Id WHERE MONTH(BillDate)=@month AND YEAR(BillDate)=YEAR(GETDATE())" +
                        " GROUP BY KH.Id, FullName, PhoneNumber" +
                        " ORDER BY ChiTieu DESC";
                    command.Parameters.Add("@month", SqlDbType.Int).Value = month;
                    using (var reader = command.ExecuteReader())
                    {
                        int stt = 0;
                        while (reader.Read())
                        {
                            CustomerStatisticsDTO customer = new CustomerStatisticsDTO()
                            {
                                Top = (++stt).ToString(),
                                FullName = reader[0].ToString(),
                                PhoneNumber = reader[1].ToString(),
                                Expenses = Convert.ToInt64(reader[2].ToString()).ToString("N0")
                            };
                            result.Add(customer);
                        }
                    }
                }
                


            } catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public List<CustomerStatisticsDTO> GetTopCustomerByYear(string year)
        {
            List<CustomerStatisticsDTO> result = new List<CustomerStatisticsDTO>();
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT FullName, PhoneNumber, SUM(Total) AS ChiTieu FROM CUSTOMER" +
                        " KH JOIN Bill BT ON KH.Id = BT.Customer_Id WHERE YEAR(BillDate)=@year" +
                        " GROUP BY KH.Id, FullName, PhoneNumber" +
                        " ORDER BY ChiTieu DESC";
                    command.Parameters.Add("@year", SqlDbType.Int).Value = year;
                    using (var reader = command.ExecuteReader())
                    {
                        int stt = 0;
                        while (reader.Read())
                        {
                            CustomerStatisticsDTO customer = new CustomerStatisticsDTO()
                            {
                                Top = (++stt).ToString(),
                                FullName = reader[0].ToString(),
                                PhoneNumber = reader[1].ToString(),
                                Expenses = Convert.ToInt64(reader[2].ToString()).ToString("N0")
                            };
                            result.Add(customer);
                        }
                    }
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
