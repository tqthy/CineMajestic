using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Views.MessageBox;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace CineMajestic.ViewModels.InformationManagement
{
    public partial class InformationViewModel
    {
        public ICommand editImageCommand {  get; set; }

        private void EditImage()
        {
            editImageCommand = new ViewModelCommand(editImage);
        }

        private void editImage(object obj)
        {
            string imageSourceOld = ImageSource;
            addImage();
            string imageSourceNew = Path.GetFileName(ImageSource);
            StaffDA staffDA = new StaffDA();
            staffDA.updateImageStaff(Staff_Id, imageSourceNew);
            if (ImageSource != imageSourceOld)
            {
                YesMessageBox mb = new YesMessageBox("Thông báo", "Đổi ảnh đại diện thành công");
                mb.ShowDialog();
            }
        }

        //add image
        private void addImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Ảnh (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                try
                {
                    string selectedImagePath = openFileDialog.FileName;
                    string folder = Path.GetDirectoryName(selectedImagePath);
                    string fileName = Path.GetFileName(selectedImagePath);
                    string extension = Path.GetExtension(fileName);//đuôi mở rộng của file


                    string fileOld1 = selectedImagePath;

                    string pathfileOld2 = selectedImagePath;

                    while (File.Exists(MotSoPTBoTro.pathProject() + @"Images\StaffManagement\" + fileName))
                    {
                        fileName = MotSoPTBoTro.RandomFileName() + extension;
                        File.Move(fileOld1, folder + @"\" + fileName);
                        pathfileOld2 = folder + @"\" + fileName;
                    }

                    try
                    {
                        MotSoPTBoTro.copyFile(pathfileOld2, MotSoPTBoTro.pathProject() + @"Images\StaffManagement");
                        ImageSource = MotSoPTBoTro.pathProject() + @"Images\StaffManagement\" + fileName;


                        //đổi lại tên file người dùng chọn
                        File.Move(pathfileOld2, selectedImagePath);
                    }
                    catch { }


                }
                catch { }
            }
        }
    }
}
