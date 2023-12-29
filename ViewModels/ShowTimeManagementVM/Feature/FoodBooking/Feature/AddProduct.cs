using CineMajestic.Models.DTOs.ProductManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.ShowTimeManagementVM
{
    public partial class FoodBookingViewModel
    {
        public ICommand AddProductCommand {  get; set; }
        public ICommand Cong1QuantityChoiceCommand {  get; set; }
        public ICommand Tru1QuantityChoiceCommand {  get; set; }

        private ObservableCollection<ProductDTO> dSSPChon;
        public ObservableCollection<ProductDTO> DSSPChon
        {
            get => dSSPChon;
            set
            {
                dSSPChon = value;
                OnPropertyChanged(nameof(DSSPChon));
            }
        }

        private void Add()
        {
            AddProductCommand = new ViewModelCommand(AddProduct);
            DSSPChon = new ObservableCollection<ProductDTO>();
            Cong1QuantityChoiceCommand = new ViewModelCommand(Cong1QuantityChoice);
            Tru1QuantityChoiceCommand = new ViewModelCommand(Tru1QuantityChoice);
        }



        private void AddProduct(object obj)
        {
            if(obj is ProductDTO productDTO)
            {
                if (!DSSPChon.Contains(productDTO))
                {
                    DSSPChon.Add(productDTO);
                }
                else
                {
                    if (productDTO.Quantity_Choice < productDTO.Quantity)
                    {
                        productDTO.Quantity_Choice += 1;
                    }
                }
            }
        }


        private void Cong1QuantityChoice(object obj)
        {
            if (obj is ProductDTO productDTO)
            {
                if (productDTO.Quantity_Choice < productDTO.Quantity)
                {
                    productDTO.Quantity_Choice += 1;
                }
            }
        }

        private void Tru1QuantityChoice(object obj)
        {
            if (obj is ProductDTO productDTO)
            {
                if (productDTO.Quantity_Choice > 1)
                {
                    productDTO.Quantity_Choice -= 1;
                }
                else 
                {
                    DSSPChon.Remove(productDTO);
                }
            }
        }
    }
}
