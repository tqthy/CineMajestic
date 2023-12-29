using CineMajestic.Models.DTOs.ProductManagement;
using CineMajestic.ViewModels.ProductManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DataAccessLayer
{
    public class ProductDA : DataAccess
    {
        public ObservableCollection<ProductDTO> getDSSP()
        {
            ObservableCollection<ProductDTO> DSSP = new ObservableCollection<ProductDTO>();

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
                            string imageSource = reader.GetString(reader.GetOrdinal("ImageSource"));
                            int quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
                            int price = reader.GetInt32(reader.GetOrdinal("Price"));
                            int type = reader.GetInt32(reader.GetOrdinal("Type"));



                            imageSource = MotSoPhuongThucBoTro.pathProject() + @"Images\ProductManagement\" + imageSource;
                            DSSP.Add(new ProductDTO(id, name, quantity, price, type, imageSource));
                        }
                    }
                }
            }
            return DSSP;
        }



        //thêm 1 sản phẩm
        public void addProduct(ProductDTO product)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string insert =
                    "insert into Product(Name,ImageSource,Quantity,Price,Type)\n"
                    +
                    "values("
                    +
                    "N'" + product.Name + "',"
                    + "'" + product.ImageSource + "',"
                    + product.Quantity.ToString() + ","
                    + product.Price.ToString() + ","
                    + product.Type.ToString() + ")";

                using (SqlCommand command = new SqlCommand(insert, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }


        //xóa 1 sản phẩm
        public void deleteProduct(ProductDTO product)
        {
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



        //update sản phẩm
        public void editProduct(ProductDTO product)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string update =
                    "update Product\n"
                    +
                    "set Name=" + "N'" + product.Name + "'" + ","
                    +
                    "Quantity=" + product.Quantity + ","
                    +
                    "Price=" + product.Price + ","
                    +
                    "Type=" + product.Type + ","
                    +
                    "ImageSource=" + "'" + product.ImageSource + "'" + "\n"
                    +
                    "where ID=" + product.Id;


                using (SqlCommand command = new SqlCommand(update, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }


        //import số lượng product
        public void importSL(ProductDTO product, int sl)
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



        //lấy danh sách sản phẩm theo loại
        public ObservableCollection<ProductDTO> getDSSPTheoLoai(int Type)//0 là thức ăn,1 là đồ uống
        {
            ObservableCollection<ProductDTO> DSSP = new ObservableCollection<ProductDTO>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string truyvan ="";
                if (Type == 2)
                {
                    truyvan = "select * from Product";
                }
                else if (Type == 1)
                {
                    truyvan = "select * from Product where Type=1";
                }
                else if (Type == 0)
                {
                    truyvan = "select * from Product where Type=0";
                }


                using (SqlCommand command = new SqlCommand(truyvan, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("ID"));
                            string name = reader.GetString(reader.GetOrdinal("Name"));
                            string imageSource = reader.GetString(reader.GetOrdinal("ImageSource"));
                            int quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
                            int price = reader.GetInt32(reader.GetOrdinal("Price"));
                            int type = reader.GetInt32(reader.GetOrdinal("Type"));



                            imageSource = MotSoPhuongThucBoTro.pathProject() + @"Images\ProductManagement\" + imageSource;
                            DSSP.Add(new ProductDTO(id, name, quantity, price, type, imageSource));
                        }
                    }
                }
            }
            return DSSP;
        }
    }
}
