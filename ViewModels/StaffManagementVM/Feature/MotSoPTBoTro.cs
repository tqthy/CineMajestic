using CineMajestic.Models.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CineMajestic.ViewModels.StaffManagementVM
{
    public class MotSoPTBoTro
    {

        //phương thức tạo tên file export ngẫy nhiên
        public static string RandomFileName()
        {
            Random random = new Random();
            int passwordLength = random.Next(10, 20);
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var password = new char[passwordLength];

            for (int i = 0; i < passwordLength; i++)
            {
                password[i] = chars[random.Next(chars.Length)];
            }
            return new string(password);
        }


        //phục vụ ValidateUsername ở add staff
        public static bool uniqueUsername(string username)
        {
            UserDA userDA = new UserDA();
            foreach(var item in userDA.selectUsername())
            {
                if (username == item)
                {
                    return false;
                }
            }
            return true;
        }




        //phương thức kiểm tra tháng này nhân viên được phát lương chưa
        public static bool checkSalary()
        {
            StaffSalaryDA staffSalaryDA = new StaffSalaryDA();
            List<string>list=staffSalaryDA.listDateBill();
            string timeNow = DateTime.Today.ToString("yyyy-MM-dd");
            foreach(var item in list)
            {
                if (item == timeNow)
                {
                    return true;
                }
            }

            return false; ;
        }
    }
}
