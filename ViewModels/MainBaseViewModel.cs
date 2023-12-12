using CineMajestic.Views;
using CineMajestic.Views.MovieManagement;
using CineMajestic.Views.VoucherManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CineMajestic.ViewModels
{
    public abstract class MainBaseViewModel : BaseViewModel
    {
        private object currentView;
        public object CurrentView
        {
            get { return currentView; }
            set { currentView = value; OnPropertyChanged(nameof(CurrentView)); }
        }

        public ICommand SwitchViewCommand { get; private set; }

        public MainBaseViewModel()
        {
            SwitchViewCommand = new ViewModelCommand(SwitchView);
        }

        private void SwitchView(object userControlName)
        {
            string UserControlName = userControlName as string;
            switch (UserControlName)
            {
                case "Movies":
                    CurrentView = new MovieManagementView();
                    break;
                case "Vouchers":
                    CurrentView = new VoucherManagement();
                    break;
            }
        }
    }
}
