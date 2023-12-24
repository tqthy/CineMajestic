using CineMajestic.Models.DataAccessLayer;
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
    public class ForgotPasswordViewModel : MainBaseViewModel
    {
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";

        private string notification;
        public string Notification
        {
            get => notification;
            set
            {
                if (notification != value)
                {
                    notification = value;
                    OnPropertyChanged(nameof(Notification));
                }
            }
        }

        private ForgetPasswordView wd;
        public ICommand AcceptCommand { get; set; }
        public ICommand backCommand { get; set; }
        public ForgotPasswordViewModel(ForgetPasswordView wd)
        {
            this.wd = wd;
            AcceptCommand = new ViewModelCommand(accept,canexecuteAccept);
            backCommand = new ViewModelCommand(back);
        }
        private void accept(object obj)
        {
            Task.Run(async () =>
            {
                Notification = "";
                if (!notify())
                {
                    Notification = "Tên tài khoản hoặc Email không tồn tại!";
                    return;
                }

                if (MotSoPTBoTro.sendMail(Username, Email))
                {
                    Notification = "Mật khẩu đã được gửi tới Email liên kết!";
                }
                else
                {
                    Notification = "Có lỗi trong quá trình tới Email liên kết!";
                }


                await Task.Delay(3000);
                Notification = "";
            });
        }

        private bool canexecuteAccept(object obj)
        {
            if (Username == "" || Email == "")
            {
                return false;
            }
            return true; 
        }

        private void back(object obj)
        {
            wd.Close();
        }


        private bool notify()
        {
            UserDA userDA = new UserDA();
            List<Tuple<string, string>> result = userDA.selectUserAndMail();
            foreach (var item in result)
            {
                if (item.Item1 == Username && item.Item2 == Email)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
