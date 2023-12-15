using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Point { get; set; }
        public string Birth { get; set; }
        public string RegDate { get; set; }
        public string Gender { get; set; }

        public CustomerDTO()
        {

        }

        public CustomerDTO(int id, string fullName, string phoneNumber, string email, int point, string birth, string regDate, string gender)
        {
            Id = id;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Email = email;
            Point = point;
            Birth = birth;
            RegDate = regDate;
            Gender = gender;
        }
        //Phục vụ edit
        public CustomerDTO(int id,string fullName, string phoneNumber, string email, string regDate, int point)
        {
            Id = id;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Email = email;
            Point = point;
            RegDate = regDate;
        }
    }
}
