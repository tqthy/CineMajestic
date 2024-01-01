using CineMajestic.Models.DataAccessLayer;
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
        public ICommand PhatLuongCommand { get; set; }

        private void phatluong()
        {
            PhatLuongCommand = new ViewModelCommand(PhatLuong);
        }

        private void PhatLuong(object obj)
        {
            string[] s = DateTime.Today.ToString("yyyy-MM-dd").Split('-');
            YesNoMessageBox wd = new YesNoMessageBox("Thông báo", "Bạn có muốn phát lương cho nhân viên không?");
            wd.ShowDialog();
            if(wd.DialogResult == false)
            {
                return;
            }
            else
            {
                if (s[2] != "20")
                {
                    wd.Close();
                    YesMessageBox mb = new YesMessageBox("Thông báo", "Hôm nay không phải ngày phát lương!");
                    mb.ShowDialog();
                    mb.Close();
                    return;
                }
                if (MotSoPTBoTro.checkSalary())
                {
                    wd.Close();
                    YesMessageBox mb = new YesMessageBox("Thông báo", "Tháng này đã phát lương rồi!");
                    mb.ShowDialog();
                    mb.Close();
                    return;
                }

                StaffSalaryDA staffSalaryDA = new StaffSalaryDA();
                staffSalaryDA.PhatLuongAll();
                wd.Close();
                YesMessageBox mb2 = new YesMessageBox("Thông báo", "Phát lương thành công!");
                mb2.ShowDialog();
                mb2.Close();
            }
            
        }
    }
}
