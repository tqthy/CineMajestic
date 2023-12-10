using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs.ProductManagement;
using CineMajestic.Views.ProductManagement;
using MaterialDesignThemes.Wpf;
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
using System.Windows.Media;

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

            //load lại danh sách
            loadData();
        }
    }


    public class AddProductViewModel : INotifyPropertyChanged
    {
        public string Name {  get; set; }

        private string imageSource;
        public string? ImageSource
        {
            get => imageSource;
            set
            {
                if (imageSource != value)
                {
                    imageSource = value;
                    OnPropertyChanged(nameof(ImageSource));
                }
            }
        }
        public int Quantity {  get; set; }
        public int Price {  get; set; }
        public int Type {  get; set; }

        AddProduct wd;//phục vụ việc đóng window
        public ICommand quitCommand { get; set; }//thoát k thêm product nữa
        public ICommand acceptCommand { get; set; }//đồng ý add
        public ICommand addImageCommand {  get; set; }//add ảnh sản phẩm

        public AddProductViewModel(AddProduct wd)
        {
            Type = Type - 1;

            quitCommand = new ViewModelCommand(quit);
            acceptCommand = new ViewModelCommand(accept);
            addImageCommand = new ViewModelCommand(addImage);
            this.wd = wd;
        }

        private void quit(object obj)
        {
            wd.Close();
        }


        //đồng ý thêm Product
        private void accept(object obj)
        {

            //nhớ sau này xử lý lỗi khi người dùng chưa nhập gì
            ImageSource = Path.GetFileName(ImageSource);
            Type += 1;
            ProductDA productDA = new ProductDA();
            productDA.addProduct(new ProductDTO(Name, Quantity, Price, Type, ImageSource));
            MessageBox.Show("Thêm thành công");
            wd.Close();
        }


        //add image
        private void addImage(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Ảnh (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                string selectedImagePath = openFileDialog.FileName;
                string fileName=Path.GetFileName(selectedImagePath);
                try
                {
                    MotSoPhuongThucBoTro.copyFile(selectedImagePath, MotSoPhuongThucBoTro.pathProject() + @"Images\ProductManagement");
                    ImageSource = MotSoPhuongThucBoTro.pathProject() + @"Images\ProductManagement\" + fileName;
                }
                catch
                {
                    MessageBox.Show("Vui lòng đổi lại tên ảnh,file ảnh bạn chọn có tên trùng với nơi lưu trữ dữ liệu!");
                    wd.Close();
                }
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
