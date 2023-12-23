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

        //Tên sản phẩm
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                ValidateName();
            }
        }
        private string nameError;
        public string NameError
        {
            get => nameError;
            set
            {
               nameError = value;
               OnPropertyChanged(nameof(NameError));
            }
        }

        //Ảnh
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

        //Quantity
        private string quantity;
        public string Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                ValidateQuantity();
            }
        }
        private string quantityError;
        public string QuantityError
        {
            get => quantityError;
            set
            {
                quantityError = value;
                OnPropertyChanged(nameof(QuantityError));
            }
        }

        //Price
        private string price;
        public string Price
        {
            get => price;
            set
            {
                price = value;
                ValidatePrice();
            }
        }
        private string priceError;
        public string PriceError
        {
            get => priceError;
            set
            {
                priceError = value;
                OnPropertyChanged(nameof(PriceError));
            }
        }
        public int Type { get; set; }

        EditProduct wd;//phục vụ việc đóng wd

        public ICommand quitCommand { get; set; }//thoát k sửa product nữa
        public ICommand addImageCommand {  get; set; }//đồng ý edit
        public ICommand acceptEditCommand { get; set; }//đồng ý edit
        public ICommand WindowClosingCommand { get; set; }

        public EditProductViewModel(EditProduct wd)
        {
            quitCommand = new ViewModelCommand(quit);  
            addImageCommand=new ViewModelCommand(addImage);
            acceptEditCommand = new ViewModelCommand(acceptEdit,canAccept);
            WindowClosingCommand = new ViewModelCommand(windowClosing);
            this.wd = wd;
            deleteImageEdit();
        }

        public void khoitao()
        {
            Name = productEdit.Name;
            Quantity=productEdit.Quantity.ToString();
            Price = productEdit.Price.ToString();
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
            int quantityNew = int.Parse(Quantity);
            int priceNew= int.Parse(Price);
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

        private void windowClosing(object obj)
        {

        }
        

        private void deleteImageEdit()
        {
            Task.Run(() =>
            {
                try
                {
                    string s = "";
                    DirectoryInfo dir = new DirectoryInfo(MotSoPhuongThucBoTro.pathProject() + @"Images\ProductManagement");
                    FileInfo[] files = dir.GetFiles("*.*", SearchOption.AllDirectories);
                    foreach (FileInfo file in files)
                    {
                        try
                        {
                            File.Delete(file.FullName);
                        }
                        catch { }
                    }
                }
                catch { }
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        //Các hàm Validate để báo lỗi

        private bool [] _canAccept = new bool[3];
        
        private bool canAccept(object obj)
        {
            return _canAccept[0] && _canAccept[1] && _canAccept[2];
        }
        private void ValidateName()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                NameError = "Tên sản phẩm không được trống!";
                _canAccept[0] = false;
            }
            else
            {
                NameError = "";
                _canAccept[0] = true;
            }
        }

        private void ValidateQuantity()
        {
            if (string.IsNullOrWhiteSpace(Quantity))
            {
                QuantityError = "Số lượng không để trống";
                _canAccept[1] = false;
            }
            else if (!Quantity.All(char.IsDigit))
            {
                QuantityError = "Số lượng không hợp lệ";
                _canAccept[1] = false;
            }
            else if (int.Parse(Quantity) < 0)
            {
                QuantityError = "Số lượng không hợp lệ";
                _canAccept[1] = false;
            }
            else
            {
                QuantityError = "";
                _canAccept[1] = true;
            }
        }
        private void ValidatePrice()
        {
            if (string.IsNullOrWhiteSpace(Price))
            {
                PriceError = "Giá không để trống";
                _canAccept[2] = false;
            }
            else if (!Price.All(char.IsDigit))
            {
                PriceError = "Giá không hợp lệ";
                _canAccept[2] = false;
            }
            else if (int.Parse(Price) < 0)
            {
                PriceError = "Giá không hợp lệ";
                _canAccept[2] = false;
            }
            else
            {
                PriceError = "";
                _canAccept[2] = true;
            }
        }
    }
}
