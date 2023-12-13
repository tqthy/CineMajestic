using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CineMajestic.Views.StaffManagement;
using MaterialDesignThemes.Wpf;

namespace CineMajestic.ViewModels.StaffManagementVM
{
    public partial class StaffManageVM
    {
        public ICommand showWdAddStaffCommand {  get; set; }
        void addStaff()
        {
            showWdAddStaffCommand = new ViewModelCommand(showWdAddStaff);
        }

        private void showWdAddStaff(object obj)
        {
            StaffAddView staffAddView = new StaffAddView();
            staffAddView.ShowDialog();

            //load lại data
            loadData();
        }
    }

    public class AddStaffViewModel : BaseViewModel
    {

        private StaffAddView wd;//phục vụ quit
        public ICommand CancelCommand { get; set; }


        public AddStaffViewModel(StaffAddView wd)
        {
            this.wd = wd;
            CancelCommand = new ViewModelCommand(cancel);
        }
        private void cancel(object obj)
        {
            wd.Close();
        }
    }
}
