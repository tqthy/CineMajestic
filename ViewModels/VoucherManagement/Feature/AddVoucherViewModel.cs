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
        public ICommand ShowWDVoucherCommand {  get; set; }
      

        void AddVoucher()
        {
            ShowWDVoucherCommand = new ViewModelCommand(ShowWDVoucher);
        }

        private void ShowWDVoucher(object obj)
        {
            AddVoucherView addVoucherView = new AddVoucherView();
            addVoucherView.ShowDialog();

            loadData();
        }
    }


    public class AddVoucherViewModel
    {
        private AddVoucherView wd;
        public ICommand quitCommand {  get; set; }
        public string Name { get; set; }
        public string VoucherDetail { get; set; }
        public string Type { get; set; }
        public DateTime ?StartDate { get; set; }
        public DateTime ?FinDate { get; set; }

        public ICommand acceptCommand {  get; set; }
        public AddVoucherViewModel(AddVoucherView wd)
        {
            this.wd= wd;
            quitCommand = new ViewModelCommand(quit);
            acceptCommand = new ViewModelCommand(accept);
        }
        private void quit(object obj)
        {
            wd.Close();
        }

        private void accept(object obj)
        {
            VoucherDA voucherDA = new VoucherDA();

            //nao nhớ fix xét code đã tồn tại hay chưa rồi hãng add
            voucherDA.addVoucher(new VoucherDTO(Name, MotSoPTBoTro.createCode(), VoucherDetail, Type, StartDate.Value.ToString("yyyy-MM-dd"), FinDate.Value.ToString("yyyy-MM-dd")));
            MessageBox.Show("Thêm thành công");
            wd.Close();
        }

    }
}
