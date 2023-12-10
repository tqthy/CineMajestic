using CineMajestic.Views.StaffManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CineMajestic.ViewModels.StaffManagementVM
{
    public class StaffManageVM:BaseViewModel 
    {
        public ICommand OpenStaffAddCommand { get; set; }
        public StaffManageVM()
        {
          //  OpenStaffAddCommand = new ViewModelCommand(OpenStaffAdd);
            StaffAdd();
        }

        void StaffAdd()
        {
            OpenStaffAddCommand = new ViewModelCommand(OpenStaffAdd);
        }

        private void OpenStaffAdd(object obj)
        {
            StaffAddView staffAdd = new StaffAddView();
            staffAdd.ShowDialog();
        }
        
        
    }
}
