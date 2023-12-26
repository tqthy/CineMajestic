using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.ViewModels
{
    public class PTChung
    {
        //kiểm tra 1 chuỗi có chứa kí tự unicode hay k
        public static bool ContainsUnicodeCharacter(string input)
        {
            foreach (char c in input)
            {
                if (c > 127)
                {
                    return true;
                }
            }

            return false;
        }



        //mã hóa md5
        public static string EncryptMD5(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }

    }
}
