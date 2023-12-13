using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.StaffManagementVM
{
    public partial class StaffManageVM
    {
        public ICommand showWdEditStaffCommand {  get; set; }

        void editStaff()
        {
            showWdEditStaffCommand = new ViewModelCommand(showWdEditStaff);
        }


        private void showWdEditStaff(object obj)
        {
            MessageBox.Show("hihi");
        }
    }
}
