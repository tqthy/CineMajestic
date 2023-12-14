using CineMajestic.Views.StaffManagement;
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
            StaffEditView staffEditView = new StaffEditView();
            staffEditView.ShowDialog();
        }
    }


    public class EditStaffViewModel
    {
        private StaffEditView wd;//phục vụ button hủy
        public ICommand quitCommand { get; set; }


        public EditStaffViewModel(StaffEditView wd)
        {
            this.wd = wd;
            quitCommand = new ViewModelCommand(quit);
        }

        private void quit(object obj)
        {
            wd.Close();
        }
    }
}
