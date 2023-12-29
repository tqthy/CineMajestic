using CineMajestic.Views.ShowTimeManagement;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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
            foodBookingView.Hide();
            foodBookingView.ShowDialog();
            try
            {
                foodBookingView.Show();
            }
            catch { }
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



    public class FoodBookingViewModel:MainBaseViewModel
    {
        public ICommand BackCommand { get; set; }


        FoodBookingView foodBookingView;
        public FoodBookingViewModel(FoodBookingView foodBookingView)
        {
            BackCommand = new ViewModelCommand(Back);
            this.foodBookingView = foodBookingView;
        }

        private void Back(object obj)
        {
            foodBookingView.Close();
        }
    }
}
