using CineMajestic.Views.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.ForgotPassword
{
    public class ForgotPasswordViewModel:MainBaseViewModel
    {
        public string Username {  get; set; }
        public string Email {  get; set; }

        private string notification;
        public string Notification
        {
            get => notification;
            set
            {
                if(notification != value)
                {
                    notification = value;
                    OnPropertyChanged(nameof(Notification));
                }
            }
        }

        private ForgetPasswordView wd;
        public ICommand AcceptCommand { get; set; }
        public ICommand backCommand {  get; set; }
        public ForgotPasswordViewModel(ForgetPasswordView wd)
        {
            this.wd = wd;
            AcceptCommand = new ViewModelCommand(accept);
            backCommand = new ViewModelCommand(back);
        }
        private void accept(object obj)
        {
            MotSoPTBoTro.sendMail(Username, Email);//sau này nhớ xử lý lỗi tài khoản có tồn tại k
            Notification = "Mật khẩu đã được gửi tới email liên kết!";
        }

        private void back(object obj)
        {
            wd.Close();
        }
    }
}
