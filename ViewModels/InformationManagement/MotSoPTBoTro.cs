using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.ViewModels.InformationManagement
{
    public class MotSoPTBoTro
    {
        public static string pathProject()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            string kq = System.IO.Path.Combine(basePath, @"..\..\..\");

            string fullPathToProject = System.IO.Path.GetFullPath(kq);
            return fullPathToProject;
        }


        public static void copyFile(string source, string destination)
        {
            if (File.Exists(source))
            {
                // Tạo đường dẫn đầy đủ cho file đích
                string destinationFilePath = Path.Combine(destination, Path.GetFileName(source));

                // Copy file
                File.Copy(source, destinationFilePath);
            }
        }


        //phương thức tạo tên file ảnh ngẫy nhiên
        public static string RandomFileName()
        {
            Random random = new Random();
            int nameLength = random.Next(10, 20);
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var name = new char[nameLength];

            for (int i = 0; i < nameLength; i++)
            {
                name[i] = chars[random.Next(chars.Length)];
            }
            return new string(name);
        }
    }
}
