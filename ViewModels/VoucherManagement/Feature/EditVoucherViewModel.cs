using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using CineMajestic.Views.VoucherManagement;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            EditVoucher  editVoucher=new EditVoucher((VoucherDTO)obj);
            editVoucher.ShowDialog();

            loadData();
        }
    }


    public class EditVoucherViewModel
    {
        private EditVoucher wd;
        public VoucherDTO voucher { get; set; }//voucher current
        public ICommand quitCommand { get; set; }
        public ICommand acceptCommand {  get; set; }
        public string Name { get; set; }
        public string VoucherDetail { get; set; }
        public string Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinDate { get; set; }

        public EditVoucherViewModel(EditVoucher wd)
        {
            this.wd = wd;
            quitCommand=new ViewModelCommand(quit);
            acceptCommand = new ViewModelCommand(accpet);
        }

        public void loadEditCurrent()
        {
            Name = voucher.Name;
            VoucherDetail = voucher.VoucherDetail;
            Type = voucher.Type;

            string[]startDate= voucher.StartDate.Split('/');
            StartDate = new DateTime(int.Parse(startDate[2]), int.Parse(startDate[1]), int.Parse(startDate[0]));

            string[] finDate = voucher.FinDate.Split('/');
            FinDate = new DateTime(int.Parse(finDate[2]), int.Parse(finDate[1]), int.Parse(finDate[0]));
        }

        private void quit(object obj)
        {
            wd.Close();
        }

        private void accpet(object obj)
        {
            VoucherDA voucherDA = new VoucherDA();

            //sau này nhớ xử lý lỗi người dùng
            voucherDA.editVoucher(new VoucherDTO(voucher.Id, Name, VoucherDetail, Type, StartDate.Value.ToString("yyyy-MM-dd"), FinDate.Value.ToString("yyyy-MM-dd")));
            MessageBox.Show("Edit thành công");
            wd.Close();
        }
    }
}
