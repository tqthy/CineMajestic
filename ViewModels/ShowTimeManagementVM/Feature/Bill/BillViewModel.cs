using CineMajestic.Views.ShowTimeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            BillView billView=new BillView();
            billView.ShowDialog();
        }
    }


    public class BillViewModel : MainBaseViewModel
    {
        public ICommand BackCommand { get; set; }



        private BillView billView;
        public BillViewModel(BillView billView)
        {
            this.billView = billView;
            BackCommand = new ViewModelCommand(Back);
        }


        private void Back(object obj)
        {
            billView.Close();
        }
    }
}
