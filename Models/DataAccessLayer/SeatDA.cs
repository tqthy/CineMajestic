using CineMajestic.Models.DTOs.ShowTimeManagement;
using MaterialDesignThemes.Wpf.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DataAccessLayer
{
    public class SeatDA
    {
        public ObservableCollection<SeatDTO> getDSGhe()
        {

            ObservableCollection<SeatDTO> list =new ObservableCollection<SeatDTO>();


            //test dữ liệu
            for(int i = 0; i < 176; i++)
            {
                list.Add(new SeatDTO(1, "A01", true, 1));
            }
            return list;
        }
    }
}
