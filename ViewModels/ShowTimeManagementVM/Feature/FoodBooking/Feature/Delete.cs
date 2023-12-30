using CineMajestic.Models.DTOs.ProductManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CineMajestic.ViewModels.ShowTimeManagementVM
{
    public partial class FoodBookingViewModel
    {
        public ICommand DeleteAllProductCommand {  get; set; }
        public ICommand DeleteProductCommand {  get; set; }


        private void Delete()
        {
            DeleteAllProductCommand=new ViewModelCommand(DeleteAllProduct);
            DeleteProductCommand=new ViewModelCommand(DeleteProduct);
        }

        private void DeleteAllProduct(object obj)
        {
            ObservableCollection<ProductDTO> list = new ObservableCollection<ProductDTO>();
            foreach(var item in DSSPChon)
            {
                list.Add(item);
            }

            foreach(var item in list)
            {
                item.Quantity_Choice = 1;
                DSSPChon.Remove(item);
            }
            TotalPrice = 0;
        }


        private void DeleteProduct(object obj)
        {
            if (obj is ProductDTO productDTO)
            {
                TotalPrice -= productDTO.Price * productDTO.Quantity_Choice;
                productDTO.Quantity_Choice = 1;
                DSSPChon.Remove(productDTO);
            }
        }
    }
}
