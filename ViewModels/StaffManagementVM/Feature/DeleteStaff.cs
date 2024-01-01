using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs.StaffManagement;
using CineMajestic.Views.MessageBox;
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
        public ICommand deleteStaffCommand {  get; set; }

        void delete()
        {
            deleteStaffCommand = new ViewModelCommand(deleteStaff);
        }

        private void deleteStaff(object obj)
        {
           // UserDA userDA = new UserDA();
            StaffDA staffDA=new StaffDA();
            if(obj is StaffDTO staff)
            {
                if (StaffId != staff.Id)
                {
                    
                    YesNoMessageBox mb = new YesNoMessageBox("Thông báo", "Bạn có muốn xóa nhân viên này?");
                    mb.ShowDialog();
                    if (mb.DialogResult == false)
                        return;
                    else
                    {
                        // userDA.deleteAccount(staff);//bởi vì ràng buộc khóa ngoại tham chiếu//dùng trigger
                        staffDA.deleteStaff(staff);
                        mb.Close();
                        YesMessageBox msb = new YesMessageBox("Thông báo", "Xóa thành công");
                        msb.ShowDialog();
                        msb.Close();
                    }
                    loadData();
                }
                else
                {
                    MessageBox.Show("Bạn không thể xóa chính mình!");
                }
            }
        }
    }
}
