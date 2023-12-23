using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using CineMajestic.ViewModels.CustomerManagement;
using CineMajestic.Views.VoucherManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.VoucherManagement
{
    public partial class VoucherManagementViewModel
    {
        public ICommand VoucherDetailCommand { get; set; }
        private void VoucherDetail()
        {
            VoucherDetailCommand = new ViewModelCommand(VoucherDetail);
        }
        private void VoucherDetail(object obj)
        {
            VoucherDTO voucherDTO = obj as VoucherDTO;
            VoucherDetailView voucherDetailView = new VoucherDetailView(voucherDTO);
            voucherDetailView.ShowDialog();
        }
    }
    public class VoucherDetailViewModel : MainBaseViewModel
    {
        private VoucherDetailView wd;

        //Nút thoát
        public ICommand exitCommand { get; set; }

        //accept
        public ICommand acceptCommand { get; set; }

        //phục vụ nội dung voucher
        private VoucherDTO getVoucher { get; set; }

        public string VoucherDetail { get; set; }



        public VoucherDetailViewModel(VoucherDTO voucher, VoucherDetailView wd)
        {
            this.wd = wd;
            exitCommand = new ViewModelCommand(exit);
            VoucherDetail = voucher.VoucherDetail;
            this.getVoucher = voucher;
            acceptCommand = new ViewModelCommand(accept);
        }

        private void exit(object obj)
        {
            wd.Close();
        }



        private void accept(object obj)
        {
            string LoaiVoucher = getVoucher.Type;

            CustomerDA customerDA = new CustomerDA();
            List<CustomerDTO> DSKHVip = new List<CustomerDTO>(customerDA.getKHVip(LoaiVoucher));//nguyên nhân em tạo mới danh sách -không xài tham chiếu vì đề phòng đang gửi mail thì có kh mới đc thêm(vì tí e xài Task)

            Task.Run(async () =>
            {
                string fromMail = "UITIT008CineMajestic@gmail.com";
                string fromPassWord = "ribrkocvhzhpuima";


                foreach (CustomerDTO item in DSKHVip)
                {
                    MailMessage mailMessage = new MailMessage();
                    try
                    {
                        mailMessage.From = new MailAddress(fromMail);
                        mailMessage.Subject = "VOUCHER MIỄN PHÍ!";
                        mailMessage.To.Add(new MailAddress(item.Email));
                    }
                    catch { }


                    MotSoPTBoTro.sendVoucherByMail(item.FullName, getVoucher, mailMessage,fromMail,fromPassWord,item,LoaiVoucher);
                }

                await Task.Delay(2000);
            });
        }
    }
}
