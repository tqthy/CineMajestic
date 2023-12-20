using CineMajestic.Models.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.ViewModels.VoucherManagement
{
    public class MotSoPTBoTro
    {

        //hàm tạo voucher
        public static string createCode()
        {
            string codeNew = "";
            
            VoucherDA voucherDA = new VoucherDA();
            List<string> listVoucher=voucherDA.listCode();

            while (true)
            {
                bool check = true;
                Random random = new Random();

                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                StringBuilder stringBuilder = new StringBuilder(10);

                for (int i = 0; i < 10; i++)
                {
                    stringBuilder.Append(chars[random.Next(chars.Length)]);
                }
                codeNew = stringBuilder.ToString();

                foreach(string s in listVoucher)
                {
                    if (codeNew == s)
                    {
                        check = false;
                        break;
                    }
                }

                if (check == true)
                {
                    break;
                }

            }
            return codeNew;
        }

        //phương thức tạo tên file excel đc ngẫy nhiên

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
