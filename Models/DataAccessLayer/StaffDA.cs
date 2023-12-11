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
            list.Add(new StaffDTO(1, "Nguyễn Văn A", "2002", "Nam", "A@gmail.com", "090988777", 10000000, 1));
            list.Add(new StaffDTO(2, "Nguyễn Văn B", "2002", "Nam", "b@gmail.com", "090988777", 20000000, 1));
            list.Add(new StaffDTO(3, "Nguyễn Văn C", "2002", "Nam", "A@gmail.com", "090988777", 10000000, 1));
            list.Add(new StaffDTO(4, "Nguyễn Văn D", "2002", "Nam", "A@gmail.com", "090988777", 10000000, 1));
            list.Add(new StaffDTO(5, "Nguyễn Văn E", "2002", "Nam", "A@gmail.com", "090988777", 10000000, 1));
            list.Add(new StaffDTO(6, "Nguyễn Văn Fddddddddddddddddddddddddddddddddddddddddd", "2002", "Nam", "A@gmail.com", "090988777", 10000000, 1));
            list.Add(new StaffDTO(7, "Nguyễn Văn G", "2002", "Nam", "A@gmail.com", "090988777", 10000000, 1));
            list.Add(new StaffDTO(8, "Nguyễn Văn A", "2002", "Nam", "A@gmail.com", "090988777", 10000000, 1));
            list.Add(new StaffDTO(9, "Nguyễn Văn B", "2002", "Nam", "b@gmail.com", "090988777", 20000000, 1));
            list.Add(new StaffDTO(10, "Nguyễn Văn C", "2002", "Nam", "A@gmail.com", "090988777", 10000000, 1));
            list.Add(new StaffDTO(11, "Nguyễn Văn D", "2002", "Nam", "A@gmail.com", "090988777", 10000000, 1));
            list.Add(new StaffDTO(12, "Nguyễn Văn E", "2002", "Nam", "A@gmail.com", "090988777", 10000000, 1));
            list.Add(new StaffDTO(13, "Nguyễn Văn Fddddddddddddddddddddddddddddddddddddddddd", "2002", "Nam", "A@gmail.com", "090988777", 10000000, 1));
            list.Add(new StaffDTO(14, "Nguyễn Văn G", "2002", "Nam", "A@gmail.com", "090988777", 10000000, 1));
            list.Add(new StaffDTO(15, "Nguyễn Văn A", "2002", "Nam", "A@gmail.com", "090988777", 10000000, 1));
            list.Add(new StaffDTO(16, "Nguyễn Văn B", "2002", "Nam", "b@gmail.com", "090988777", 20000000, 1));
            list.Add(new StaffDTO(17, "Nguyễn Văn C", "2002", "Nam", "A@gmail.com", "090988777", 10000000, 1));
            list.Add(new StaffDTO(18, "Nguyễn Văn D", "2002", "Nam", "A@gmail.com", "090988777", 10000000, 1));
            list.Add(new StaffDTO(19, "Nguyễn Văn E", "2002", "Nam", "A@gmail.com", "090988777", 10000000, 1));
            list.Add(new StaffDTO(20, "Nguyễn Văn Fddddddddddddddddddddddddddddddddddddddddd", "2002", "Nam", "A@gmail.com", "090988777", 10000000, 1));
            list.Add(new StaffDTO(21, "Nguyễn Văn G", "2002", "Nam", "A@gmail.com", "090988777", 10000000, 1)); ;
            return list;
        }
    }
}
