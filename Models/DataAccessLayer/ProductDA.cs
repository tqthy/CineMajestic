﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineMajestic.Models.DTOs;
using CineMajestic.Models.DTOs.ProductManagement;
using CineMajestic.ViewModels.ProductManagement;

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
                            DSSP.Add(new ProductDTO(id,name,quantity,price,type,imageSource));
                        }
                    }
                }
            }
            return DSSP;
        }



        //thêm 1 sản phẩm
        public void addProduct(ProductDTO product)
        {
            using(SqlConnection connection = GetConnection())
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

                using(SqlCommand command=new SqlCommand(insert,connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }


        //xóa 1 sản phẩm
        public void deleteProduct(ProductDTO product)
        {
            using(SqlConnection connection = GetConnection())
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
            using(SqlConnection connection=GetConnection())
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
    }
}
