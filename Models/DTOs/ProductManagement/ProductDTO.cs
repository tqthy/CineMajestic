using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DTOs.ProductManagement
{

    //test để xem hiển thị UI đúng k
    public class ProductDTO 
    {
        private int id;
        private string name;
        private int quantity;
        private int price;
        private int type;//1 là thức ăn,2 là đồ uống
        private string imageSource;

        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int Quantity { get { return quantity; } set { quantity = value; } }
        public int Price { get { return price; } set { price = value; } }
        public int Type { get { return type; } set { type = value; } }
        public string ImageSource { get { return imageSource; } set { imageSource = value; } }

        public ProductDTO(int id, string name, int quantity, int price, int type,string imageSource)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Price = price;
            Type = type;
            ImageSource = imageSource;
        }

    }
}
