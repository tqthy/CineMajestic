using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs.ProductManagement;
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

        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity= value;
                OnPropertyChanged(nameof(Quantity));
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
            quitCommand=new ViewModelCommand(quit);
            acceptImportCommand=new ViewModelCommand(acceptImport);
        }

        private void quit(object obj)
        {
            wd.Close();
        }

        private void acceptImport(object obj)
        {
            //xử lý lỗi nhập người dùng đã nhé

            ProductDA productDA = new ProductDA();
            productDA.importSL(product, Quantity);

            MessageBox.Show("Nhập thành công");
            wd.Close();
        }
    }
}
