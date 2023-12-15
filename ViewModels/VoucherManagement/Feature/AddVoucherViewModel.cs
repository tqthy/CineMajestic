using CineMajestic.Views.VoucherManagement;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CineMajestic.ViewModels.VoucherManagement
{
    public partial class VoucherManagementViewModel
    {
        public ICommand ShowWDVoucherCommand {  get; set; }

        void AddVoucher()
        {
            ShowWDVoucherCommand = new ViewModelCommand(ShowWDVoucher);
        }

        private void ShowWDVoucher(object obj)
        {
            AddVoucherView addVoucherView = new AddVoucherView();
            addVoucherView.ShowDialog();
        }
    }


    public class AddVoucherViewModel
    {
        private AddVoucherView wd;
        public ICommand quitCommand {  get; set; }

        public AddVoucherViewModel(AddVoucherView wd)
        {
            this.wd= wd;
            quitCommand = new ViewModelCommand(quit);
        }
        private void quit(object obj)
        {
            wd.Close();
        }
    }
}
