using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs.ProductManagement;
using CineMajestic.Views.MessageBox;
using CineMajestic.Views.ProductManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.ProductManagement
{
    public partial class ProductManagementViewModel
    {
        public ICommand importQuantityCommand {  get; set; }

        private void import()
        {
            importQuantityCommand = new ViewModelCommand(importQuantity);
        }

        private void importQuantity(object obj)
        {
            ImportProduct importProduct = new ImportProduct(obj as ProductDTO);
            importProduct.ShowDialog();
            loadData();
        }
    }


    public class ImportProductViewModel:MainBaseViewModel
    {

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
        private string  quantityError;
        public string QuantityError
        {
            get => quantityError;
            set
            {
                quantityError = value;
                OnPropertyChanged(nameof(QuantityError));
            }
        }

        ImportProduct wd;
        ProductDTO product;
        public ICommand quitCommand { get; set; }
        public ICommand acceptImportCommand {  get; set; }
        public ImportProductViewModel(ImportProduct wd,ProductDTO product)
        {
            this.wd = wd;
            this.product = product;
            Quantity = product.Quantity.ToString();
            Quantity = "";
            quitCommand=new ViewModelCommand(quit);
            acceptImportCommand=new ViewModelCommand(acceptImport,canAccept);
        }

        private void quit(object obj)
        {
            wd.Close();
        }

        private void acceptImport(object obj)
        {
            //xử lý lỗi nhập người dùng đã nhé

            ProductDA productDA = new ProductDA();
            productDA.importSL(product,int.Parse(Quantity));

            YesMessageBox mb = new YesMessageBox("Thông báo", "Nhập thêm số lượng sản phẩm thành công");
            mb.ShowDialog();
            wd.Close();
        }

        //hàm Validate
        private bool _canAccept;
        private bool canAccept(object obj)
        {
            return _canAccept;
        }
        private void ValidateQuantity()
        {
            if (string.IsNullOrWhiteSpace(Quantity))
            {
                QuantityError = "Số lượng không để trống";
                _canAccept = false;
            }
            else if (!Quantity.All(char.IsDigit))
            {
                QuantityError = "Số lượng không hợp lệ";
                _canAccept = false;
            }
            else if (int.Parse(Quantity) < 0) 
            {
                QuantityError = "Số lượng không hợp lệ";
                _canAccept = false;
            }
            else
            {
                QuantityError = "";
                _canAccept = true;
            }
        }
    }
}
