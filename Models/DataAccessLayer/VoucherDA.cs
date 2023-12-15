using CineMajestic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CineMajestic.Models.DataAccessLayer
{
    public class VoucherDA:DataAccess
    {

        //lấy data từ bảng Voucher
        public ObservableCollection<VoucherDTO> getDSVC()
        {
            ObservableCollection<VoucherDTO> list = new ObservableCollection<VoucherDTO>();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string truyvan = "select * from Voucher";
                using (SqlCommand command = new SqlCommand(truyvan, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read()) 
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("ID"));

                            string name = reader.GetString(reader.GetOrdinal("NAME"));

                            string code= reader.GetString(reader.GetOrdinal("CODE"));

                            string voucherDetail = reader.GetString(reader.GetOrdinal("VOUCHERDETAIL"));

                            string type = reader.GetString(reader.GetOrdinal("TYPE"));

                            DateTime StartDate = reader.GetDateTime(reader.GetOrdinal("STARTDATE"));
                            string startDate = StartDate.ToString("dd/MM/yyyy");

                            DateTime FinDate = reader.GetDateTime(reader.GetOrdinal("FINDATE"));
                            string finDate = FinDate.ToString("dd/MM/yyyy");

                            list.Add(new VoucherDTO(id, name, code, voucherDetail, type, startDate, finDate));
                        }
                    }
                }
            }
            return list;
        }

    }
}
