using CineMajestic.Models.DataAccessLayer;
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
            if (s[2] != "20")
            {
                MessageBox.Show("Hôm nay không phải ngày phát lương!");
                return;
            }
            if (MotSoPTBoTro.checkSalary())
            {
                MessageBox.Show("Tháng này đã phát lương rồi");
                return;
            }

            StaffSalaryDA staffSalaryDA = new StaffSalaryDA();
            staffSalaryDA.PhatLuongAll();

            MessageBox.Show("Thành công!");
        }
    }
}
