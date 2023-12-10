using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CineMajestic.ViewModels.StaffManagementVM
{
    public class StaffManageVM :BaseViewModel
    {
        public ICommand OpenStaffAdd { get; set; }        

        public StaffManageVM() 
        {
            ViewModelCommand OpenStaffAdd;
            
        }
        
    }
}
