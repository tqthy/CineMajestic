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
        public ICommand showWdAddStaffCommand {  get; set; }
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


       

        //error nhập lại mật khẩu 




        public ICommand CancelCommand { get; set; }
        public ICommand acceptAddCommand { get; set; }//đồng ý thêm


        public AddStaffViewModel(StaffAddView wd)
        {
            this.wd = wd;
            CancelCommand = new ViewModelCommand(cancel);
            acceptAddCommand = new ViewModelCommand(accpetAdd);

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
        private void accpetAdd(object obj)
        {
            ////sau này nhớ xử lý lỗi người dùng k nhập gì,nhập sai
            if (Gender == null)
            {
                MessageBox.Show("Vui lòng chọn giới tính");
                return;
            }
            if (Role == null)
            {
                MessageBox.Show("Vui lòng chọn chức vụ");
                return;
            }
            if (Birth == null)
            {
                MessageBox.Show("Vui lòng chọn ngày sinh");
                return;
            }
            if (NgayVL == null)
            {
                MessageBox.Show("Vui lòng chọn ngày vào làm");
                return;
            }
            if (wd.txtMatKhau.Password != wd.txtNhapLaiMatKhau.Password)
            {
                MessageBox.Show("2 mật khẩu không trùng nhau");
                return;
            }

            //nhớ xử lý ràng buộc unique account rồi mới add staff
            //cách đẹp nhất là truy vấn bảng account rồi xét

            //add vào bảng Staff
            StaffDA staffDA = new StaffDA();
            staffDA.addStaff(new StaffDTO(FullName, Birth.Value.ToString("yyyy-MM-dd"), Gender, Email, PhoneNumber, int.Parse(Salary), Role, NgayVL.Value.ToString("yyyy-MM-dd")));

            //add vào bảng Account,sau này nhớ mã hóa pass ở đây luôn
            UserDA userDA = new UserDA();
            userDA.addAccount(new UserDTO(Username, wd.txtMatKhau.Password, staffDA.identCurrent()));

            MessageBox.Show("Thêm nhân viên thành công");
            wd.Close();

        }



        //Các hàm Validate
        private void ValidateFullName()
        {
            if (string.IsNullOrWhiteSpace(FullName))
            {
                FullNameError = "Họ và tên không được để trống!";
            }
            else
            {
                FullNameError = "";
            }
        }


        private void ValidateBirth()
        {
            if (Birth>DateTime.UtcNow)
            {
                BirthError = "Ngày sinh không hợp lệ!";
            }
            else
            {
                BirthError = "";
            }
        }

        private void ValidateEmail()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                EmailError = "Email không được để trống!";
            }
            else if (!Email.Contains("@"))
            {
                EmailError = "Email không hợp lệ!";
            }
            else
            {
                EmailError = "";
            }
        }

        private void ValidatePhoneNumber()
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                PhoneNumberError = "SĐT không được để trống!";
            }
            else if (!PhoneNumber.All(char.IsDigit))
            {
                PhoneNumberError = "Số điện thoại chỉ được chứa chữ số!";
            }
            else
            {
                PhoneNumberError = "";
            }
        }


        private void ValidateNgayVL()
        {
            if (NgayVL < Birth)
            {
                NgayVLError = "Ngày đăng ký phải lớn hơn ngày sinh!";
            }
            else
            {
                NgayVLError = "";
            }
        }


        private void ValidateSalary()
        {
            if (string.IsNullOrWhiteSpace(Salary))
            {
                SalaryError = "Lương không hợp lệ!";
            }
            else if (!Salary.All(char.IsDigit))
            {
                SalaryError = "Lương không hợp lệ!";
            }
            else if (int.Parse(Salary) < 0)
            {
                SalaryError = "Lương không hợp lệ!";
            }
            else
            {
                SalaryError = "";
            }
        }


        private void ValidateUsername()
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                UsernameError = "Username không được để trống!";
            }
            else if(Username.Contains(" "))
            {
                UsernameError = "Username không hợp lệ!";
            }
            else if (Username.Length < 6)
            {
                UsernameError = "Username phải lớn hơn 5 kí tự!";
            }
            else
            {
                UsernameError = "";
            }
        }



        //mật khẩu
        private void ValidatePassword1()
        {

        }

        //nhập lại mật khẩu
        private void ValidatePassword2()
        {

        }
    }
}
