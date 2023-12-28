using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs.ShowTimeManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.ViewModels.ShowTimeManagementVM
{
    public partial class ShowTimeManagementViewModel : MainBaseViewModel
    {
        private ObservableCollection<ShowTimeDTO> dSSuatChieu;
        public ObservableCollection<ShowTimeDTO> DSSuatChieu
        {
            get => dSSuatChieu;
            set
            {
                dSSuatChieu = value;
                OnPropertyChanged(nameof(DSSuatChieu));
            }
        }

        public ShowTimeManagementViewModel()
        {
            loadData();//lần đầu mở thì vào phần all
            Auditorium();
        }

        private void loadData(string Phong="All")//load data theo phòng
        {
            ShowTimeDA showTimeDA = new ShowTimeDA();
            DSSuatChieu = showTimeDA.getDSShowTime(Phong);
        }
    }
}
