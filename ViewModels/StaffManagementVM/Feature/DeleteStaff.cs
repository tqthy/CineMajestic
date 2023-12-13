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
        public ICommand deleteStaffCommand {  get; set; }

        void delete()
        {
            deleteStaffCommand = new ViewModelCommand(deleteStaff);
        }

        private void deleteStaff(object obj)
        {
            MessageBox.Show("hihi");
        }
    }
}
