using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CineMajestic.Models.DataAccessLayer;

namespace CineMajestic.ViewModels.ForgotPassword
{
    public class MotSoPTBoTro
    {

        public static void sendMail(string username,string mailReceive)
        {
            try
            {
                string fromMail = "UITIT008CineMajestic@gmail.com";
                string fromPassWord = "ribrkocvhzhpuima";

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromMail);
                mailMessage.Subject = "Khôi phục mật khẩu";
                mailMessage.To.Add(new MailAddress(mailReceive));


                string noidung = "Mật khẩu mới của bạn là: ";
                string passwordNew = RandomPasswordNew();
                mailMessage.Body = "<html><body>" + noidung + passwordNew + "</body></html>";
                mailMessage.IsBodyHtml = true;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassWord),
                    EnableSsl = true
                };
                smtpClient.Send(mailMessage);


                //sửa lại password trong bảng account theo username
                //nhớ sau này làm xong cái  mã hóa thì phải ép từ đây
                UserDA userDA = new UserDA();
                userDA.changePassword(username, passwordNew);
            }
            catch{ }
        }


        //hàm tạo ngẫu nhiên password mới
        public static string RandomPasswordNew()
        {
            Random random = new Random();
            int passwordLength = random.Next(5, 11);
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
