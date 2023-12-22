using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.ViewModels.InformationManagement
{
    public class InformationViewModel:MainBaseViewModel
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Birth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string NgayVL { get; set; }
        public int Salary { get; set; }

        private string imageSource;
        public string ImageSource
        {
            get => imageSource;
            set
            {
                imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }
        public InformationViewModel()
        {
            loadData();
        }

        private void loadData()
        {
            //tạm thời dùng dữ liệu mẫu vì chưa xử cái login
            FullName = "Nguyễn Văn a";
            Gender = "Nam";
            Birth = "10/10/1999";
            Email = "a@gmail.com";
            PhoneNumber= "1234567890";
            Role="Quản lý";
            NgayVL = "12/12/2023";
            Salary = 9999999;
            ImageSource = "pack://application:,,,/Images/InformationManagement/Default.jpg";
        }
    }
}
