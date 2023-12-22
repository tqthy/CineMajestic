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
        public ICommand VoucherDetailCommand {  get; set; }
        private void VoucherDetail()
        {
            VoucherDetailCommand = new ViewModelCommand(VoucherDetail);
        }
        private void VoucherDetail(object obj)
        {
            VoucherDTO voucherDTO=obj as VoucherDTO;
            VoucherDetailView voucherDetailView =new VoucherDetailView(voucherDTO);
            voucherDetailView.ShowDialog();
        }
    }
}
