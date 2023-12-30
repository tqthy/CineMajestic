using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs.Bills;
using CineMajestic.Models.DTOs.ProductManagement;
using CineMajestic.Views.ShowTimeManagement;
using MaterialDesignThemes.Wpf;
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
    public partial class TicketBookingViewModel
    {
        public ICommand ContinueCommand { get; set; }

        private void FoodBooking()
        {
            ContinueCommand = new ViewModelCommand(Continue,CanContinue);
        }

        private void Continue(object obj)
        {
            orderDTO.Seats = Seats;
            orderDTO.TotalTicket = TotalPriceTicket;
            FoodBookingView foodBookingView = new FoodBookingView(orderDTO);
            foodBookingView.ShowDialog();
        }

        private bool CanContinue(object obj)
        {
            if(Seats== "Hiện chưa chọn ghế nào!")
            {
                return false;
            }
            return true;
        }
    }



    public partial class FoodBookingViewModel:MainBaseViewModel
    {
        public ICommand BackCommand { get; set; }
        private ObservableCollection<ProductDTO> dSSP;
        public ObservableCollection<ProductDTO> DSSP
        {
            get => dSSP;
            set
            {
                dSSP = value;
                OnPropertyChanged(nameof(DSSP));    
            }
        }


        FoodBookingView foodBookingView;
        private OrderDTO orderDTO;
        public FoodBookingViewModel(FoodBookingView foodBookingView,OrderDTO orderDTO)
        {
            BackCommand = new ViewModelCommand(Back);
            this.foodBookingView = foodBookingView;
            this.orderDTO = orderDTO;
            loadDSSP(0);//ban đầu là get all sản phẩm
            Filter();
            Add();
            Delete();
            Bill();
        }

        private void Back(object obj)
        {
            foodBookingView.Close();
        }

        private void loadDSSP(int type)
        {
            ProductDA productDA = new ProductDA();
            DSSP = productDA.getDSSPTheoLoai(type);
        }
    }
}
