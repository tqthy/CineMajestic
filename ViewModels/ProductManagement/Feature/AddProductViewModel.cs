using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs.ProductManagement;
using CineMajestic.Views.MessageBox;
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


    public class AddProductViewModel : MainBaseViewModel
    {
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

        //PurchasePrice
        private string purchasePrice;
        public string PurchasePrice
        {
            get => purchasePrice;
            set
            {
                purchasePrice = value;
                ValidatePurchasePrice();
            }
        }
        private string purchasePriceError;
        public string PurchasePriceError
        {
            get => purchasePriceError;
            set
            {
                purchasePriceError = value;
                OnPropertyChanged(nameof(PurchasePriceError));
            }
        }
        public int Type {  get; set; }

        AddProduct wd;//phục vụ việc đóng window
        public ICommand quitCommand { get; set; }//thoát k thêm product nữa
        public ICommand acceptCommand { get; set; }//đồng ý add
        public ICommand addImageCommand {  get; set; }//add ảnh sản phẩm

        public AddProductViewModel(AddProduct wd)
        {
            Type =0;
            quitCommand = new ViewModelCommand(quit);
            acceptCommand = new ViewModelCommand(accept,canAccept);
            addImageCommand = new ViewModelCommand(addImage);
            this.wd = wd;
            deleteImage();
        }

        private void quit(object obj)
        {
            wd.Close();
        }


        //đồng ý thêm Product
        private void accept(object obj)
        {

            ImageSource = Path.GetFileName(ImageSource);
            Type += 1;
            ProductDA productDA = new ProductDA();
            productDA.addProduct(new ProductDTO(Name, int.Parse(Quantity), int.Parse(PurchasePrice), Type, ImageSource));
            YesMessageBox mb = new YesMessageBox("Thông báo", "Thêm sản phẩm thành công");
            mb.ShowDialog();
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
                try
                {
                    string selectedImagePath = openFileDialog.FileName;
                    string folder = Path.GetDirectoryName(selectedImagePath);
                    string fileName = Path.GetFileName(selectedImagePath);
                    string extension = Path.GetExtension(fileName);//đuôi mở rộng của file


                    string fileOld1 = selectedImagePath;

                    string pathfileOld2 = selectedImagePath;

                    while (File.Exists(MotSoPhuongThucBoTro.pathProject() + @"Images\ProductManagement\" + fileName))
                    {
                        fileName = MotSoPhuongThucBoTro.RandomFileName() + extension;
                        File.Move(fileOld1, folder + @"\" + fileName);
                        pathfileOld2 = folder + @"\" + fileName;
                    }

                    try
                    {
                        MotSoPhuongThucBoTro.copyFile(pathfileOld2, MotSoPhuongThucBoTro.pathProject() + @"Images\ProductManagement");
                        ImageSource = MotSoPhuongThucBoTro.pathProject() + @"Images\ProductManagement\" + fileName;


                        //đổi lại tên file người dùng chọn
                        File.Move(pathfileOld2, selectedImagePath);
                    }
                    catch { }


                }
                catch { }
            }
        }



        //cái này là phục vụ cho cái deleteproduct,editproduct,thêm product
        private void deleteImage()
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

        //Các hàm Validate để báo lỗi

        private bool[] _canAccept = new bool[3];

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
        private void ValidatePurchasePrice()
        {
            if (string.IsNullOrWhiteSpace(PurchasePrice))
            {
                PurchasePriceError = "Giá không để trống";
                _canAccept[2] = false;
            }
            else if (!PurchasePrice.All(char.IsDigit))
            {
                PurchasePriceError = "Giá không hợp lệ";
                _canAccept[2] = false;
            }
            else if (!int.TryParse(PurchasePrice, out int PurchasePriceValue))
            {
                PurchasePriceError = "Không hợp lệ!";
                _canAccept[2] = false;
            }
            else
            {
                PurchasePriceError = "";
                _canAccept[2] = true;
            }
        }
    }
}
