using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs.StaffManagement;
using CineMajestic.Views.StaffManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.StaffManagementVM
{
    public partial class StaffManageVM
    {
        public ICommand showWdEditStaffCommand {  get; set; }

        void editStaff()
        {
            showWdEditStaffCommand = new ViewModelCommand(showWdEditStaff);
        }


        private void showWdEditStaff(object obj)
        {
            StaffDTO staff=obj as StaffDTO;
            StaffEditView staffEditView = new StaffEditView(staff);
            staffEditView.ShowDialog();
            loadData();
        }
    }


    public class EditStaffViewModel :MainBaseViewModel
    {
        private StaffEditView wd;//phục vụ button hủy
        public ICommand quitCommand { get; set; }//hủy k edit nữa
        public ICommand acceptCommand { get; set; }//xác nhận edit

        public StaffDTO staffDTO { get; set; }//phục vụ việc hiển thị staff hiện tại khi nhấn vào edit

        public EditStaffViewModel(StaffEditView wd)
        {
            this.wd = wd;
            quitCommand = new ViewModelCommand(quit);
            acceptCommand = new ViewModelCommand(accept,canAccept);
        }

        public void khoitao()
        {
            FullName = staffDTO.FullName;
            Gender=staffDTO.Gender;
            
            string []birth=staffDTO.Birth.Split('/');
            Birth=new DateTime(int.Parse(birth[2]), int.Parse(birth[1]), int.Parse(birth[0]));

            Email = staffDTO.Email;
            PhoneNumber = staffDTO.PhoneNumber;
            Role = staffDTO.Role;

            string[]ngayvl=staffDTO.NgayVaoLam.Split("/");
            NgayVL=new DateTime(int.Parse(ngayvl[2]), int.Parse(ngayvl[1]), int.Parse(ngayvl[0]));

            Salary=staffDTO.Salary.ToString();
        }
        private void quit(object obj)
        {
            wd.Close();
        }

        private void accept(object obj)
        {
            StaffDA staffDA = new StaffDA();
            staffDA.updateStaff(new StaffDTO(staffDTO.Id, FullName, Birth.Value.ToString("yyyy-MM-dd"), Gender, Email, PhoneNumber, int.Parse(Salary), Role, NgayVL.Value.ToString("yyyy-MM-dd")));
            MessageBox.Show("Sửa nhân viên thành công");
            wd.Close();
        }

        //Họ và tên
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

        //Giới tính
        public string Gender { get; set; }

        ////Ngày sinh(làm kiểu bắt lỗi cho ngày nhỏ hơn ngày hiện tại)
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

        //SĐT
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

        //Lương
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

        private bool[] _canAccept = new bool[6];//phục vụ xác nhận edit
        private bool canAccept(object obj)
        {
            return _canAccept[0] && _canAccept[1] && _canAccept[2] && _canAccept[3] && _canAccept[4] &&_canAccept[5];
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
                NgayVLError = "Ngày vào làm phải lớn hơn ngày sinh!";
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
                SalaryError = "Lương không được để trống!";
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
    }
}
