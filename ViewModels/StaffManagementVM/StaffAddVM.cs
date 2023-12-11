using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.StaffManagementVM
{
    public class StaffAddVM: BaseViewModel
    {
        public ICommand CancelCommand { get; set; }
        public ICommand ConFirmCommand { get; set; }

        public StaffAddVM() 
        {
            
        }

     
    }
}
