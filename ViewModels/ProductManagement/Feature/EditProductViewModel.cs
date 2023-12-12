using CineMajestic.Views.ProductManagement;
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
}
