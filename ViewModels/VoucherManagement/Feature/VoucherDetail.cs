﻿using CineMajestic.Models.DTOs;
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
    public class VoucherDetailViewModel : MainBaseViewModel
    {
        private VoucherDetailView wd;

        //Nút thoát
        public ICommand exitCommand { get; set; }

        //accept
        public ICommand acceptCommand {  get; set; }

        //phục vụ nội dung voucher
        private VoucherDTO getVoucher {  get; set; }

        public string VoucherDetail {  get; set; }

        

        public VoucherDetailViewModel(VoucherDTO voucher,VoucherDetailView wd)
        {
            this.wd = wd;
            exitCommand = new ViewModelCommand(exit);
            VoucherDetail = voucher.VoucherDetail;
            this.getVoucher = voucher;
        }

        private void exit(object obj)
        {
            wd.Close();
        }



        private void accept(object obj)
        {

        }
    }
}
