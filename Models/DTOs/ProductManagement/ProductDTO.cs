using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DTOs.ProductManagement
{
    public class ProductDTO 
    {
        private int id;
        private string name;
        private int quantity;
        private int purchasePrice;
        private int price;
        private int type;//1 là thức ăn,2 là đồ uống
        private string imageSource;

        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int Quantity { get { return quantity; } set { quantity = value; } }
        public int PurchasePrice { get { return purchasePrice; } set {  purchasePrice = value; } }
        public int Price { get { return price; } set { price = value; } }
        public int Type { get { return type; } set { type = value; } }
        public string ImageSource { get { return imageSource; } set { imageSource = value; } }


        //constructor phục vụ việc getdata
        public ProductDTO(int id, string name, int quantity,int purchasePrice, int price, int type,string imageSource)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            PurchasePrice = purchasePrice;
            Price = price;
            Type = type;
            ImageSource = imageSource;
        }


        //constructor phục vụ việc add 1 product
        public ProductDTO(string name, int quantity, int purchasePrice, int type, string imageSource)
        {
            Name = name;
            Quantity = quantity;
            PurchasePrice = purchasePrice;
            Type = type;
            ImageSource = imageSource;
        }


        //constructor phục vụ việc edit 1 product
        public ProductDTO(int id, string name, int quantity, int purchasePrice, int type, string imageSource)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            PurchasePrice = purchasePrice;
            Type = type;
            ImageSource = imageSource;
        }
    }
}
