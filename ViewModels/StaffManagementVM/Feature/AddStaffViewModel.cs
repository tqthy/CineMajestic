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
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime? Birth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public DateTime? NgayVL { get; set; }
        public int Salary { get; set; }

        //add bảng account
        public string Username { get; set; }

        public ICommand CancelCommand { get; set; }
        public ICommand acceptAddCommand { get; set; }//đồng ý thêm


        public AddStaffViewModel(StaffAddView wd)
        {
            this.wd = wd;
            CancelCommand = new ViewModelCommand(cancel);
            acceptAddCommand = new ViewModelCommand(accpetAdd);
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
            staffDA.addStaff(new StaffDTO(FullName, Birth.Value.ToString("yyyy-MM-dd"), Gender, Email, PhoneNumber, Salary, Role, NgayVL.Value.ToString("yyyy-MM-dd")));

            //add vào bảng Account,sau này nhớ mã hóa pass ở đây luôn
            UserDA userDA = new UserDA();
            userDA.addAccount(new UserDTO(Username, wd.txtMatKhau.Password, staffDA.identCurrent()));

            MessageBox.Show("Thêm nhân viên thành công");
            wd.Close();

        }

    }
}
