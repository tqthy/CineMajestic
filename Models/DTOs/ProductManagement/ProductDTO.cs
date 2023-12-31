﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CineMajestic.Models.DTOs.ProductManagement
{
    public class ProductDTO : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private int quantity;
        private int price;
        private int purchasePrice;
        private int type;//1 là thức ăn,2 là đồ uống
        private BitmapImage imageSource;
        private int quantity_Choice = 1;//dùng cho phần order thôi
        private int totalPrice_QuantityChoice;//dùng cho phần order thôi


        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }

        public int Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }
        public int PurchasePrice { get { return purchasePrice; } set { purchasePrice = value; } }

        public int Quantity_Choice//dùng cho phần order thôi
        {
            get => quantity_Choice;
            set
            {
                quantity_Choice = value;
                OnPropertyChanged(nameof(Quantity_Choice));
            }
        }
        public int TotalPrice_QuantityChoice//dùng cho phần order thôi
        {
            get => totalPrice_QuantityChoice;
            set
            {
                totalPrice_QuantityChoice = value;
                OnPropertyChanged(nameof(TotalPrice_QuantityChoice));
            }
        }


        public int Price { get { return price; } set { price = value; } }
        public int Type { get { return type; } set { type = value; } }
        public BitmapImage ImageSource { get { return imageSource; } set { imageSource = value; } }

        //constructor phục vụ việc getdata
        public ProductDTO(int id, string name, int quantity, int purchasePrice, int price, int type, BitmapImage imageSource)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            PurchasePrice = purchasePrice;
            Price = price;
            Type = type;
            ImageSource = imageSource;
        }

        //constructor phục vụ việc edit 1 product
        public ProductDTO(int id, string name, int quantity, int purchasePrice, int type, BitmapImage imageSource)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            PurchasePrice = purchasePrice;
            Type = type;
            ImageSource = imageSource;
        }
        //constructor phục vụ việc add 1 product
        public ProductDTO(string name, int quantity, int purchasePrice, int type, BitmapImage imageSource)
        {
            Name = name;
            Quantity = quantity;
            PurchasePrice = purchasePrice;
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