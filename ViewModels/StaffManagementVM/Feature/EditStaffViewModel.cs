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


    public class EditStaffViewModel
    {
        private StaffEditView wd;//phục vụ button hủy
        public ICommand quitCommand { get; set; }//hủy k edit nữa
        public ICommand acceptCommand { get; set; }//xác nhận edit

        public StaffDTO staffDTO { get; set; }//phục vụ việc hiển thị staff hiện tại khi nhấn vào edit
        public string FullName {  get; set; }
        public string Gender { get; set; }
        public DateTime? Birth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public DateTime? NgayVL { get; set; }
        public int Salary { get; set; }


        public EditStaffViewModel(StaffEditView wd)
        {
            this.wd = wd;
            quitCommand = new ViewModelCommand(quit);
            acceptCommand = new ViewModelCommand(accept);
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

            Salary=staffDTO.Salary;
        }
        private void quit(object obj)
        {
            wd.Close();
        }

        private void accept(object obj)
        {
            StaffDA staffDA = new StaffDA();
            staffDA.updateStaff(new StaffDTO(staffDTO.Id, FullName, Birth.Value.ToString("yyyy-MM-dd"), Gender, Email, PhoneNumber, Salary, Role, NgayVL.Value.ToString("yyyy-MM-dd")));
            MessageBox.Show("Sửa nhân viên thành công");
            wd.Close();
        }
    }
}
