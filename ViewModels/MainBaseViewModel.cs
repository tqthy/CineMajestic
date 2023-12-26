
using CineMajestic.Views;
using CineMajestic.Views.ErrorManagement;
using CineMajestic.Models.DTOs;
using CineMajestic.Views.InformationManagement;
using CineMajestic.Views.MovieManagement;
using CineMajestic.Views.VoucherManagement;
using CineMajestic.Views.StaffManagement;
using CineMajestic.Views.ShowTimeManagement;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CineMajestic.Views.ProductManagement;
using CineMajestic.Views.CustomerManagement;
using OfficeOpenXml.Packaging.Ionic.Zip;

namespace CineMajestic.ViewModels
{
    public abstract class MainBaseViewModel :BaseViewModel
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

        private int Staff_Id;

        public MainBaseViewModel(int Staff_Id)
        {
            SwitchViewCommand = new ViewModelCommand(SwitchView);
            this.Staff_Id = Staff_Id;
        }

        private void SwitchView(object userControlName)
        {
            string UserControlName = userControlName as string;
            switch (UserControlName)
            {
                case "Movies":
                    CurrentView = new MovieManagementView();
                    break;
                case "Error":
                    CurrentView = new ErrorManagementView();
                    break;
                case "ShowTime":
                    CurrentView = new ShowTimeManagementView();
                    break;
                case "Staff": // Quản lí nhân viên
                    CurrentView = new StaffManagementView();
                    break;
                case "QLSP"://quản lý sản phẩm    
                    CurrentView = new ProductManagementView();
                    break;
                case "Customer":
                    CurrentView = new CustomerManagementView();
                    break;
                case "Vouchers":
                    CurrentView = new VoucherManagementView();
                    break;
                case "Personal": // Cài đặt
                    CurrentView = new InformationView(Staff_Id);
                    break;
            }
        }
    }
}
