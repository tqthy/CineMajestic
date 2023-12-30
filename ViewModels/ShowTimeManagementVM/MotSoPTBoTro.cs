using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.ViewModels.ShowTimeManagementVM
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

    }
}
