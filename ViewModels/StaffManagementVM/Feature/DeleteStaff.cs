﻿using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs.StaffManagement;
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
            UserDA userDA = new UserDA();
            StaffDA staffDA=new StaffDA();
            if(obj is StaffDTO staff)
            {
                userDA.deleteAccount(staff);//bởi vì ràng buộc khóa ngoại tham chiếu
                staffDA.deleteStaff(staff);
                MessageBox.Show("Xóa nhân viên thành công");
                loadData();
            }
        }
    }
}