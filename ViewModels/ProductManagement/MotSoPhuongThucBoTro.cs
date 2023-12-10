using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.ViewModels.ProductManagement
{
    public class MotSoPhuongThucBoTro
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
    }
}
