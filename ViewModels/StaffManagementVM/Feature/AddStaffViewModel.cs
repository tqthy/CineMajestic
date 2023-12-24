using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using CineMajestic.Models.DTOs.StaffManagement;
using CineMajestic.Views.StaffManagement;
using MaterialDesignThemes.Wpf;

namespace CineMajestic.ViewModels.StaffManagementVM
{
    public partial class StaffManageVM
    {
        public ICommand showWdAddStaffCommand { get; set; }
        void addStaff()
        {
            showWdAddStaffCommand = new ViewModelCommand(showWdAddStaff);
        }

        private void showWdAddStaff(object obj)
        {
            StaffAddView staffAddView = new StaffAddView();
            staffAddView.ShowDialog();

            //load lại data
            loadData();
        }
    }

    public class AddStaffViewModel : MainBaseViewModel
    {

        private StaffAddView wd;//phục vụ quit và add 


        //add cho bảng Staff

        //họ và tên
        private string fullName;
        public string FullName
        {
            get => fullName;
            set
            {
                fullName = value;
                ValidateFullName();
            }
        }

        private string fullNameError;
        public string FullNameError
        {
            get => fullNameError;
            set
            {
                fullNameError = value;
                OnPropertyChanged(nameof(FullNameError));
            }
        }



        //giới tính
        public string Gender { get; set; }



        //Ngày sinh(làm kiểu bắt lỗi cho ngày nhỏ hơn ngày hiện tại)
        private DateTime? birth;
        public DateTime? Birth
        {
            get => birth;
            set
            {
                birth = value;
                ValidateBirth();
            }
        }

        private string birthError;
        public string BirthError
        {
            get => birthError;
            set
            {
                birthError = value;
                OnPropertyChanged(nameof(BirthError));
            }
        }




        //Email
        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                ValidateEmail();
            }
        }
        private string emailError;
        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged(nameof(EmailError));
            }
        }




        //Số điện thoại
        private string phoneNumber;
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                ValidatePhoneNumber();
            }
        }

        private string phoneNumberError;
        public string PhoneNumberError
        {
            get => phoneNumberError;
            set
            {
                phoneNumberError = value;
                OnPropertyChanged(nameof(PhoneNumberError));
            }
        }




        //Chức vụ

        public string Role { get; set; }



        //ngày vào làm
        private DateTime? ngayVL;
        public DateTime? NgayVL
        {
            get => ngayVL;
            set
            {
                ngayVL = value;
                ValidateNgayVL();
            }
        }

        private string ngayVLError;
        public string NgayVLError
        {
            get => ngayVLError;
            set
            {
                ngayVLError = value;
                OnPropertyChanged(nameof(NgayVLError));
            }
        }





        //lương
        private string salary;
        public string Salary
        {
            get => salary;
            set
            {
                salary = value;
                ValidateSalary();
            }
        }
        private string salaryError;
        public string SalaryError
        {
            get => salaryError;
            set
            {
                salaryError = value;
                OnPropertyChanged(nameof(SalaryError));
            }
        }




        //add bảng account

        //tên tài khoản
        private string username;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                ValidateUsername();
            }
        }

        private string usernameError;
        public string UsernameError
        {
            get => usernameError;
            set
            {
                usernameError = value;
                OnPropertyChanged(nameof(UsernameError));
            }
        }




        //error nhập mật khẩu 
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


        //error nhập lại mật khẩu
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



        private bool[] _canAccept = new bool[7];//phục vụ việc có cho nhấn button accept k

        public ICommand CancelCommand { get; set; }
        public ICommand acceptAddCommand { get; set; }//đồng ý thêm


        public AddStaffViewModel(StaffAddView wd)
        {
            this.wd = wd;
            CancelCommand = new ViewModelCommand(cancel);
            acceptAddCommand = new ViewModelCommand(acceptAdd, canAcceptAdd);

            Gender = "Nam";
            Role = "Quản lý";
            Birth = DateTime.UtcNow;
            NgayVL = DateTime.UtcNow;
        }
        private void cancel(object obj)
        {
            wd.Close();
        }

        //đồng ý thêm 
        private void acceptAdd(object obj)
        {

            if (!ValidatePassword1())
            {
                return;
            }
            if (!ValidatePassword2()) { return; }


            //add vào bảng Staff
            StaffDA staffDA = new StaffDA();
            staffDA.addStaff(new StaffDTO(FullName, Birth.Value.ToString("yyyy-MM-dd"), Gender, Email, PhoneNumber, int.Parse(Salary), Role, NgayVL.Value.ToString("yyyy-MM-dd")));

            //add vào bảng Account,sau này nhớ mã hóa pass ở đây luôn
            UserDA userDA = new UserDA();
            userDA.addAccount(new UserDTO(Username, wd.txtMatKhau.Password, staffDA.identCurrent()));

            MessageBox.Show("Thêm nhân viên thành công");
            wd.Close();

        }

        private bool canAcceptAdd(object obj)
        {
            bool check= _canAccept[0] && _canAccept[1] && _canAccept[2] && _canAccept[3] && _canAccept[4] && _canAccept[5] && _canAccept[6];
            if(check==false || wd.txtMatKhau.Password == "" || wd.txtNhapLaiMatKhau.Password == "")
            {
                return false;
            }
            return true;
        }

        //Các hàm Validate
        private void ValidateFullName()
        {
            if (string.IsNullOrWhiteSpace(FullName))
            {
                FullNameError = "Họ và tên không được để trống!";
                _canAccept[0] = false;
            }
            else
            {
                FullNameError = "";
                _canAccept[0] = true;
            }
        }


        private void ValidateBirth()
        {
            if (Birth > DateTime.UtcNow)
            {
                BirthError = "Ngày sinh không hợp lệ!";
                _canAccept[1] = false;
            }
            else
            {
                BirthError = "";
                _canAccept[1] = true;
            }
        }

        private void ValidateEmail()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                EmailError = "Email không được để trống!";
                _canAccept[2] = false;
            }
            else if (!Email.Contains("@"))
            {
                EmailError = "Email không hợp lệ!";
                _canAccept[2] = false;
            }
            else
            {
                EmailError = "";
                _canAccept[2] = true;
            }
        }

        private void ValidatePhoneNumber()
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                PhoneNumberError = "SĐT không được để trống!";
                _canAccept[3] = false;
            }
            else if (!PhoneNumber.All(char.IsDigit))
            {
                PhoneNumberError = "Số điện thoại chỉ được chứa chữ số!";
                _canAccept[3] = false;
            }
            else
            {
                PhoneNumberError = "";
                _canAccept[3] = true;
            }
        }


        private void ValidateNgayVL()
        {
            if (NgayVL < Birth)
            {
                NgayVLError = "Ngày đăng ký phải lớn hơn ngày sinh!";
                _canAccept[4] = false;
            }
            else
            {
                NgayVLError = "";
                _canAccept[4] = true;
            }
        }


        private void ValidateSalary()
        {
            if (string.IsNullOrWhiteSpace(Salary))
            {
                SalaryError = "Lương không hợp lệ!";
                _canAccept[5] = false;
            }
            else if (!Salary.All(char.IsDigit))
            {
                SalaryError = "Lương không hợp lệ!";
                _canAccept[5] = false;
            }
            else if (int.Parse(Salary) < 0)
            {
                SalaryError = "Lương không hợp lệ!";
                _canAccept[5] = false;
            }
            else
            {
                SalaryError = "";
                _canAccept[5] = true;
            }
        }


        private void ValidateUsername()
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                UsernameError = "Username không được để trống!";
                _canAccept[6] = false;
            }
            else if (Username.Contains(" "))
            {
                UsernameError = "Username không hợp lệ!";
                _canAccept[6] = false;
            }
            else if (Username.Length < 6)
            {
                UsernameError = "Username phải lớn hơn 6 kí tự!";
                _canAccept[6] = false;
            }
            else if (!MotSoPTBoTro.uniqueUsername(Username))
            {
                UsernameError = "Username đã tồn tại!";
                _canAccept[6] = false;
            }
            else
            {
                UsernameError = "";
                _canAccept[6] = true;
            }
        }


        
        private bool ValidatePassword1()
        {
            if (wd.txtMatKhau.Password.Length < 5)
            {
                Password1Error = "Mật khẩu phải lớn hơn 5 kí tự!";
                return false;
            }
            else if (wd.txtMatKhau.Password.Contains(" "))
            {
                Password1Error = "Mật khẩu không được chứa khoảng trắng!";
                return false;
            }
            else if (PTChung.ContainsUnicodeCharacter(wd.txtMatKhau.Password))
            {
                Password1Error = "Mật khẩu không được có dấu!";
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
            if (wd.txtMatKhau.Password != wd.txtNhapLaiMatKhau.Password)
            {
                Password2Error = "Mật khẩu không khớp,vui lòng nhập lại!";
                return false;
            }
            else
            {
                Password2Error = "";
                return true;
            }
        }
    }
}
