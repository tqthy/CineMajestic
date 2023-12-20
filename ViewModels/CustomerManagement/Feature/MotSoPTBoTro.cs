using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.ViewModels.CustomerManagement
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
    }
}
