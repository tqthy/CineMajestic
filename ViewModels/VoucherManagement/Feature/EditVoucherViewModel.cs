using CineMajestic.Models.DTOs;
using CineMajestic.Views.VoucherManagement;
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
        public ICommand showWdEditVoucherCommand { get; set; }

        void editVoucher()
        {
            showWdEditVoucherCommand = new ViewModelCommand(showWdEditVoucher);
        }

        private void showWdEditVoucher(object obj)
        {
            EditVoucher  editVoucher=new EditVoucher();
            editVoucher.ShowDialog();

            loadData();
        }
    }
}
