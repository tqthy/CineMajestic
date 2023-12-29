using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DTOs.ProductManagement
{
    public class ProductDTO:INotifyPropertyChanged
    {
        private int id;
        private string name;
        private int quantity;
        private int price;
        private int type;//1 là thức ăn,2 là đồ uống
        private string imageSource;
        private int quantity_Choice = 1;


        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }

        public int Quantity
        {
            get => quantity;
            set
            {
                quantity=value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public int Quantity_Choice
        {
            get => quantity_Choice;
            set
            {
                quantity_Choice=value;
                OnPropertyChanged(nameof(Quantity_Choice));
            }
        }


        public int Price { get { return price; } set { price = value; } }
        public int Type { get { return type; } set { type = value; } }
        public string ImageSource { get { return imageSource; } set { imageSource = value; } }

        public ProductDTO(int id, string name, int quantity, int price, int type, string imageSource)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Price = price;
            Type = type;
            ImageSource = imageSource;
        }


        //constructor phục vụ việc add 1 product
        public ProductDTO(string name, int quantity, int price, int type, string imageSource)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            Type = type;
            ImageSource = imageSource;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
