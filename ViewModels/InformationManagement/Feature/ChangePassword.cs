using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Views.MessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.InformationManagement
{
    public partial class InformationViewModel
    {
        public ICommand changePasswordCommand { get; set; }

        //error nhập mật khẩu ban đầu
        private string password1Error;
        public string Password1Error
        {
            get => password1Error;
            set
            {
                password1Error = value;
                OnPropertyChanged(nameof(Password1Error));
            }
        }


        //error nhập mật khẩu mới
        private string password2Error;
        public string Password2Error
        {
            get => password2Error;
            set
            {
                password2Error = value;
                OnPropertyChanged(nameof(Password2Error));
            }
        }

        //error nhập lại mật khẩu mới
        private string password3Error;
        public string Password3Error
        {
            get => password3Error;
            set
            {
                password3Error = value;
                OnPropertyChanged(nameof(Password3Error));
            }
        }


        private void ChangePassword()
        {
            changePasswordCommand = new ViewModelCommand(changePassword, canChangePassword);
        }

        private void changePassword(object obj)
        {
            if (!ValidatePassword1())
            {
                return;
            }
            if (!ValidatePassword2()) { return; }
            if (!ValidatePassword3()) { return; }

            UserDA userDA = new UserDA();
            userDA.changePassword(Staff_Id,PTChung.EncryptMD5(informationView.txtMKMoi.Password));
            YesMessageBox mb = new YesMessageBox("Thông báo", "Đổi mật khẩu thành công");
            mb.ShowDialog();
            informationView.txtMKCu.Password = "";
            informationView.txtMKMoi.Password = "";
            informationView.txtXacNhanMKMoi.Password = "";
        }

        private bool canChangePassword(object obj)
        {
            if (informationView.txtMKCu.Password == "" || informationView.txtMKMoi.Password == "" || informationView.txtXacNhanMKMoi.Password == "")
            {
                return false;
            }
            return true;
        }



        //các hàm validate
        private bool ValidatePassword1()
        {
            UserDA userDA = new UserDA();
            if (PTChung.EncryptMD5(informationView.txtMKCu.Password) != userDA.passwordStaff_Id(Staff_Id))
            {
                Password1Error = "Mật khẩu cũ không đúng,vui lòng thử lại!";
                return false;
            }
            else
            {
                Password1Error = "";
                return true;
            }
        }


        private bool ValidatePassword2()
        {
            if (informationView.txtMKMoi.Password.Length < 5)
            {
                Password2Error = "Mật khẩu phải lớn hơn 5 kí tự!";
                return false;
            }
            else if (informationView.txtMKMoi.Password.Contains(" "))
            {
                Password2Error = "Mật khẩu không được chứa khoảng trắng!";
                return false;
            }
            else if (PTChung.ContainsUnicodeCharacter(informationView.txtMKMoi.Password))
            {
                Password2Error = "Mật khẩu không được có dấu!";
                return false;
            }
            else
            {
                Password2Error = "";
                return true;
            }
        }


        private bool ValidatePassword3()
        {
            if (informationView.txtMKMoi.Password!=informationView.txtXacNhanMKMoi.Password)
            {
                Password3Error = "Không trùng với mật khẩu mới!";
                return false;
            }
            else
            {
                Password3Error = "";
                return true;
            }
        }
    }
}
