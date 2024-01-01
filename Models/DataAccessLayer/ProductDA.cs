using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using CineMajestic.Models.DTOs;
using CineMajestic.Models.DTOs.ProductManagement;
using CineMajestic.ViewModels;
using CineMajestic.ViewModels.ProductManagement;

namespace CineMajestic.Models.DataAccessLayer
{

    public class ProductDA : DataAccess
    {
        public ObservableCollection<ProductDTO> getDSSP()
        {
            ObservableCollection<ProductDTO> DSSP = new ObservableCollection<ProductDTO>();

            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string truyvan = "select * from Product";
                    using (SqlCommand command = new SqlCommand(truyvan, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal("ID"));
                                string name = reader.GetString(reader.GetOrdinal("Name"));

                                byte[] imageBytes = (byte[])reader["ImageSource"]; 
                                BitmapImage imageSource =ImageVsSQL.ByteArrayToBitmapImage(imageBytes); 
                                
                                int quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
                                int purchasePrice = reader.GetInt32(reader.GetOrdinal("PurchasePrice"));
                                int price = reader.GetInt32(reader.GetOrdinal("Price"));
                                int type = reader.GetInt32(reader.GetOrdinal("Type"));

                                DSSP.Add(new ProductDTO(id, name, quantity, purchasePrice, price, type, imageSource));
                            }
                        }
                    }
                }
            }
            catch { }

            return DSSP;
        }



        //thêm 1 sản phẩm
        public void addProduct(ProductDTO product)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                 
                    byte[] imageBytes = ImageVsSQL.BitmapImageToByteArray(product.ImageSource);

                    string insert = "INSERT INTO Product (Name, ImageSource, Quantity, PurchasePrice, Type) VALUES (@Name, @ImageSource, @Quantity, @PurchasePrice, @Type)";

                    using (SqlCommand command = new SqlCommand(insert, connection))
                    {
                        command.Parameters.AddWithValue("@Name", product.Name);
                        command.Parameters.AddWithValue("@ImageSource", imageBytes);
                        command.Parameters.AddWithValue("@Quantity", product.Quantity);
                        command.Parameters.AddWithValue("@PurchasePrice", product.PurchasePrice);
                        command.Parameters.AddWithValue("@Type", product.Type);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }


        //xóa 1 sản phẩm
        public void deleteProduct(ProductDTO product)
        {
            try {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string delete =
                        "delete Product\n"
                        +
                        "where ID=" + product.Id;

                    using (SqlCommand command = new SqlCommand(delete, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
            
        }



        //update sản phẩm
        public void editProduct(ProductDTO product)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    byte[] imageBytes = ImageVsSQL.BitmapImageToByteArray(product.ImageSource);

                    string update = "UPDATE Product SET Name = @Name, Quantity = @Quantity, PurchasePrice = @PurchasePrice, Type = @Type, ImageSource = @ImageSource WHERE ID = @Id";

                    using (SqlCommand command = new SqlCommand(update, connection))
                    {
                        command.Parameters.AddWithValue("@Name", product.Name);
                        command.Parameters.AddWithValue("@Quantity", product.Quantity);
                        command.Parameters.AddWithValue("@PurchasePrice", product.PurchasePrice);
                        command.Parameters.AddWithValue("@Type", product.Type);
                        command.Parameters.AddWithValue("@ImageSource", imageBytes);
                        command.Parameters.AddWithValue("@Id", product.Id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }


        //import số lượng product
        public void importSL(ProductDTO product, int sl)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    int slnew = sl + product.Quantity;
                    connection.Open();
                    string update =
                        "update Product\n"
                        +
                        "set Quantity=" + slnew
                        +
                        "where ID=" + product.Id;


                    using (SqlCommand command = new SqlCommand(update, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }

        public List<ProductStatisticsDTO> GetTopProductByMonth(string month)
        {
            List<ProductStatisticsDTO> results = new List<ProductStatisticsDTO>();
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT Name, SUM(BD.Quantity) AS BuyCount, SUM(BD.Total) AS Income FROM BillDetail BD " +
                        "JOIN Product PD ON BD.Product_Id=PD.ID JOIN Bill ON BD.Bill_Id=Bill.Id " +
                        "WHERE MONTH(BillDate)=@month AND YEAR(BillDate)=YEAR(GETDATE()) GROUP BY PD.ID, Name";
                    command.Parameters.Add("@month", SqlDbType.Int).Value = month;
                    using (var reader = command.ExecuteReader())
                    {
                        int rank = 0;
                        while (reader.Read())
                        {
                            ProductStatisticsDTO product = new ProductStatisticsDTO()
                            {
                                Rank = (++rank).ToString(),
                                Name = reader[0].ToString(),
                                BuyCount = reader[1].ToString(),
                                Income = Convert.ToInt64(reader[2].ToString()).ToString("N0")
                            };
                            results.Add(product);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return results;

        }

        public List<ProductStatisticsDTO> GetTopProductByYear(string year)
        {
            List<ProductStatisticsDTO> results = new List<ProductStatisticsDTO>();
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT Name, SUM(BD.Quantity) AS BuyCount, SUM(BD.Total) AS Income FROM BillDetail BD " +
                        "JOIN Product PD ON BD.Product_Id=PD.ID JOIN Bill ON BD.Bill_Id=Bill.Id " +
                        "WHERE YEAR(BillDate)=@year GROUP BY PD.ID, Name";
                    command.Parameters.Add("@year", SqlDbType.Int).Value = year;
                    using (var reader = command.ExecuteReader())
                    {
                        int rank = 0;
                        while (reader.Read())
                        {
                            ProductStatisticsDTO product = new ProductStatisticsDTO()
                            {
                                Rank = (++rank).ToString(),
                                Name = reader[0].ToString(),
                                BuyCount = reader[1].ToString(),
                                Income = Convert.ToInt64(reader[2].ToString()).ToString("N0")
                            };
                            results.Add(product);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return results;
        }


        //lấy danh sách sản phẩm theo loại
        public ObservableCollection<ProductDTO> getDSSPTheoLoai(int Type)//1 là thức ăn,2 là đồ uống
        {
            ObservableCollection<ProductDTO> DSSP = new ObservableCollection<ProductDTO>();
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string truyvan = "";
                    if (Type == 0)
                    {
                        truyvan = "select * from Product";
                    }
                    else if (Type == 1)
                    {
                        truyvan = "select * from Product where Type=1";
                    }
                    else if (Type == 2)
                    {
                        truyvan = "select * from Product where Type=2";
                    }


                    using (SqlCommand command = new SqlCommand(truyvan, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal("ID"));
                                string name = reader.GetString(reader.GetOrdinal("Name"));

                                byte[] imageBytes = (byte[])reader["ImageSource"];
                                BitmapImage imageSource = ImageVsSQL.ByteArrayToBitmapImage(imageBytes);

                                int quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
                                int purchasePrice = reader.GetInt32(reader.GetOrdinal("PurchasePrice"));
                                int price = reader.GetInt32(reader.GetOrdinal("Price"));
                                int type = reader.GetInt32(reader.GetOrdinal("Type"));


                                DSSP.Add(new ProductDTO(id, name, quantity, purchasePrice, price, type, imageSource));
                            }

                        }
                    }
                }
            }
            catch { }
            return DSSP;
        }


        //cập nhật lại số lượng product sau khi thanh toán hóa đơn
        public void updateQuantity(int id,int quantity)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {

                    connection.Open();
                    string update =
                        "update Product\n"
                        +
                        "set Quantity=Quantity - " + quantity + "\n"
                        +
                        "where ID=" + id;


                    using (SqlCommand command = new SqlCommand(update, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }
    }
}
