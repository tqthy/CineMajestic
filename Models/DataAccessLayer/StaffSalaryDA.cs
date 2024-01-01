using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DataAccessLayer
{
    public class StaffSalaryDA:DataAccess
    {
        //phát lương all
        public void PhatLuongAll()
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string pro = "execute sp_phatluong_staff_salary";
                    using (SqlCommand command = new SqlCommand(pro, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }


        //lấy toàn bộ ngày tháng năm phát lương của bill
        public List<string> listDateBill()
        {
            List<string> list = new List<string>();
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string truyvan = "select BillDate from Staff_Salary";
                    using (SqlCommand command = new SqlCommand(truyvan, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime billDate = reader.GetDateTime(reader.GetOrdinal("BillDate"));
                                string BillDate = billDate.ToString("yyyy-MM-dd");
                                string[] s = BillDate.Split(' ');
                                list.Add(s[0]);
                            }
                        }
                    }
                }
            }
            catch { }


            return list;
        }
    }
}
