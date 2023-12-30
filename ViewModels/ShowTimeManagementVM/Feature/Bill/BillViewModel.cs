using CineMajestic.Models.DTOs.Bills;
using CineMajestic.Views.ShowTimeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.ShowTimeManagementVM
{
    public partial class FoodBookingViewModel
    {
        public ICommand ContinueCommand { get; set; }


        private void Bill()
        {
            ContinueCommand = new ViewModelCommand(Continue);
        }

        private void Continue(object obj)
        {
            orderDTO.TotalProduct = TotalPrice;
            BillView billView=new BillView(orderDTO);
            billView.ShowDialog();
        }
    }


    public class BillViewModel : MainBaseViewModel
    {
        public ICommand BackCommand { get; set; }
        public ICommand Paycommand {  get; set; }


        private BillView billView;
        private OrderDTO orderDTO;
        public BillViewModel(BillView billView,OrderDTO orderDTO)
        {
            this.billView = billView;
            this.orderDTO= orderDTO;
            BackCommand = new ViewModelCommand(Back);
            Paycommand = new ViewModelCommand(Pay);
        }


        private void Back(object obj)
        {
            billView.Close();
        }
        
        private void Pay(object obj)
        {
           
        }
    }
}
