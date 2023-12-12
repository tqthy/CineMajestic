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
using System.Windows.Media;

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
            //truyền ID để phục vụ update
            ProductDTO product=obj as ProductDTO;
            EditProduct editProduct = new EditProduct(product);


            editProduct.ShowDialog();
            //load lại danh sách
            loadData();
        }
    }

    public class EditProductViewModel:INotifyPropertyChanged
    {
        public ProductDTO productEdit {  get; set; }
        public string Name { get; set; }

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
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Type { get; set; }

        EditProduct wd;//phục vụ việc đóng wd

        public ICommand quitCommand { get; set; }//thoát k sửa product nữa
        public ICommand addImageCommand {  get; set; }//đồng ý edit
        public ICommand acceptEditCommand { get; set; }//đồng ý edit


        public EditProductViewModel(EditProduct wd)
        {
            quitCommand = new ViewModelCommand(quit);  
            addImageCommand=new ViewModelCommand(addImage);
            acceptEditCommand = new ViewModelCommand(acceptEdit);
            this.wd = wd;

        }

        public void khoitao()
        {
            Name = productEdit.Name;
            Quantity=productEdit.Quantity;
            Price = productEdit.Price;
            Type = productEdit.Type - 1;
            ImageSource= productEdit.ImageSource;
        }

        //thoát(hủy k edit nữa)
        private void quit(object obj)
        {
            wd.Close();
        }


        //accept edit
        private void acceptEdit(object obj)
        {
            string nameNew = Name;
            int quantityNew = Quantity;
            int priceNew= Price;
            int typeNew = Type + 1;
            string imageSourceNew = Path.GetFileName(ImageSource);

            ProductDA productDA = new ProductDA();
            productDA.editProduct(new ProductDTO(productEdit.Id,nameNew, quantityNew, priceNew, typeNew, imageSourceNew));
            MessageBox.Show("Sửa thành công");
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
                string fileName = Path.GetFileName(selectedImagePath);
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
