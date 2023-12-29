using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DTOs.ShowTimeManagement
{
    public class SeatDTO
    {
        public int Id {  get; set; }
        public string Location {  get; set; }
        public bool Condition { get; set; }
        public int Auditorium_Id {  get; set; }

        //constructor phục vụ lấy danh sách ghế

        public SeatDTO(int id, string location, bool condition, int auditorium_Id)
        {
            Id = id;
            Location = location;
            Condition = condition;
            Auditorium_Id = auditorium_Id;
        }
    }
}
