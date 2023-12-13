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
            list.Add(new StaffDTO(1, "Nguyễn Văn A", "24/8/2002", "Nam", "A@gmail.com", "090988777", 10000000, "Quản lí", "2000/10/01"));
            list.Add(new StaffDTO(2, "Nguyễn Văn B", "1/2/2002", "Nam", "B@gmail.com", "090988777", 10000000, "Nhân viên", "2000/10/01"));
            list.Add(new StaffDTO(3, "Nguyễn Văn C", "3/5/2002", "Nam", "C@gmail.com", "090988777", 10000000, "Quản lí","2000/10/01"));
            return list;
        }
    }
}
