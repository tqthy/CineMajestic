using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineMajestic.Models.DTOs.StaffManagement;

namespace CineMajestic.Models.DataAccessLayer
{
    public class StaffDA
    {
        public static ObservableCollection<StaffDTO> getDSNV()
        {
            ObservableCollection<StaffDTO>list = new ObservableCollection<StaffDTO>();
            list.Add(new StaffDTO(1, "Nguyễn Văn A", "2002", "Nam", "A@gmail.com", "090988777", 10000000, "Quản lí","2000/10/01"));
            return list;
        }
    }
}
