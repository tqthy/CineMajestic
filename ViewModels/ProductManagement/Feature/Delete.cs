﻿using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs.ProductManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.ProductManagement
{
    public partial class ProductManagementViewModel
    {
        public ICommand deleteProductCommand { get; set; }

        void delete()
        {
            deleteProductCommand = new ViewModelCommand(deleteProduct);
        }
        private void deleteProduct(object obj)
        {
            ProductDA productDA = new ProductDA();
            if (obj is ProductDTO product)
            {
                productDA.deleteProduct(product);
                loadData();
            }
        }
    }
}
