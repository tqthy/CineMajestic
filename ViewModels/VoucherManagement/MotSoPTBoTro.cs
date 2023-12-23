using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CineMajestic.ViewModels.VoucherManagement
{
    public class MotSoPTBoTro
    {

        //hàm tạo voucher
        public static string createCode()
        {
            string codeNew = "";

            VoucherDA voucherDA = new VoucherDA();
            List<string> listVoucher = voucherDA.listCode();

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

                foreach (string s in listVoucher)
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



        //hàm tạo QRCode
        private static MemoryStream memoryImage(string noidungQR)
        {
            QRCodeGenerator QG = new QRCodeGenerator();
            var myData = QG.CreateQrCode(noidungQR, QRCodeGenerator.ECCLevel.H);
            var code = new QRCode(myData);
            using (var bitmap = code.GetGraphic(50))
            {
                var memory = new MemoryStream();
                try
                {
                    bitmap.Save(memory, ImageFormat.Png);
                }
                catch { }
                return memory;
            }
        }


        //hàm gửi voucher bằng mail cho 1 khách hàng
        public static void sendVoucherByMail(string fullName,VoucherDTO voucherDTO)
        {
            string noidung =
                "Cinema UIT trân trọng gửi đến quý khách hàng: " + fullName + ".\n"
                +"Voucher ngày: " + voucherDTO.StartDate + ".\n"
                +"Tên voucher: " + voucherDTO.Name + ".\n" 
                +"Thông tin Voucher: " + voucherDTO.VoucherDetail + ".\n"
                +"Ngày hết hạn: " + voucherDTO.FinDate + ".\n";

            //tạm thời như này : khi merge vào Custom thì làm tiếp
        }
    }
}
