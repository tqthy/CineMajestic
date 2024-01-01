using CineMajestic.Models.DTOs.ProductManagement;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DataAccessLayer.Bills
{
    public class BillDetailDA:DataAccess
    {
        public void addBillDetail(int bill_Id,ProductDTO productDTO)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string insert =
                        "insert into BillDetail(Bill_Id,Product_Id,Quantity,UnitPrice)\n"
                        +
                        "values("
                        +
                        bill_Id + ","
                        +
                        productDTO.Id + ","
                        +
                        productDTO.Quantity_Choice + ","
                        +
                        productDTO.Price + ")";
                    using (SqlCommand command = new SqlCommand(insert, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }
    }
}
