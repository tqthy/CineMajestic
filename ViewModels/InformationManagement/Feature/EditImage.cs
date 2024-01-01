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
        public ICommand editImageCommand { get; set; }

        private void EditImage()
        {
            editImageCommand = new ViewModelCommand(editImage);
        }



        private void editImage(object obj)
        {
            addImage();
            StaffDA staffDA = new StaffDA();
            staffDA.updateImageStaff(Staff_Id, ImageSource);
            YesMessageBox mb = new YesMessageBox("Thông báo", "Đổi ảnh đại diện thành công");
            mb.ShowDialog();
        }

        //add image
        private void addImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";

            byte[] imageData;

            if (openFileDialog.ShowDialog() == true)
            {
                imageData = File.ReadAllBytes(openFileDialog.FileName);

                ImageSource = ImageVsSQL.ByteArrayToBitmapImage(imageData);
            }
        }
    }
}
