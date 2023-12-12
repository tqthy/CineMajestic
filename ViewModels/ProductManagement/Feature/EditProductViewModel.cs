using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs.ProductManagement;
using CineMajestic.Views.ProductManagement;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.ProductManagement
{
    public partial class ProductManagementViewModel
    {
        public ICommand showWDEditProductCommand { get; set; }

        void editProduct()
        {
            showWDEditProductCommand = new ViewModelCommand(ShowWDEditProduct);
        }
        private void ShowWDEditProduct(object obj)
        {
            EditProduct editProduct = new EditProduct();
            editProduct.ShowDialog();

            //load lại danh sách
            loadData();
        }
    }

    public class EditProductViewModel:INotifyPropertyChanged
    {
       
        EditProduct wd;//phục vụ việc đóng wd

        public ICommand quitCommand { get; set; }//thoát k sửa product nữa
    
        public EditProductViewModel(EditProduct wd)
        {
            quitCommand = new ViewModelCommand(quit);     
            this.wd = wd;
        }

        private void quit(object obj)
        {
            wd.Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
