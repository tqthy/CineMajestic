using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs.ProductManagement;
using CineMajestic.Views.ShowTimeManagement;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            FoodBookingView foodBookingView = new FoodBookingView();
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
        public FoodBookingViewModel(FoodBookingView foodBookingView)
        {
            BackCommand = new ViewModelCommand(Back);
            this.foodBookingView = foodBookingView;

            loadDSSP(2);//ban đầu là get all sản phẩm
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
