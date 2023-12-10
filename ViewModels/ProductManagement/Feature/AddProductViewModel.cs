using CineMajestic.Views.ProductManagement;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CineMajestic.ViewModels.ProductManagement
{
    public partial class ProductManagementViewModel
    {
        public ICommand showWDAddProductCommand { get; set; }

        void addProduct()
        {
            showWDAddProductCommand = new ViewModelCommand(ShowWDAddProduct);
        }
        private void ShowWDAddProduct(object obj)
        {
            AddProduct addProduct = new AddProduct();
            addProduct.ShowDialog();
        }
    }


    public class AddProductViewModel
    {
        AddProduct wd;//phục vụ việc đóng window
        public ICommand quitCommand { get; set; }
        
        public AddProductViewModel(AddProduct wd)
        {
            quitCommand = new ViewModelCommand(quit);
            this.wd = wd;
        }

        private void quit(object obj)
        {
            wd.Close();
        }
    }
}
