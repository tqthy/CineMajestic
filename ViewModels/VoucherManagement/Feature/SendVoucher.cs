using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
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
        //phục vụ thông báo send mail thành công 
        private string notify;
        public string Notify
        {
            get { return notify; }
            set
            {
                notify = value;
                OnPropertyChanged(nameof(Notify));
            }
        }


        public ICommand sendVoucherCommand {  get; set; }

        private void SendVoucher()
        {
            sendVoucherCommand = new ViewModelCommand(sendVoucher);
        }
        private void sendVoucher(object obj)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn gửi Voucher tới mail khách hàng không?", "Gửi voucher", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {

                VoucherDTO getVoucher = obj as VoucherDTO;
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


                        MotSoPTBoTro.sendVoucherByMail(item.FullName, getVoucher, mailMessage, fromMail, fromPassWord, item, LoaiVoucher);
                    }

                    Notify = "Gửi Voucher tới mail khách hàng thành công!";
                    await Task.Delay(2000);
                    Notify = "";
                });
            }
        }
    }
}
