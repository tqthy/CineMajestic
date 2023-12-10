using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DTOs.StaffManagement
{
    public class StaffDTO
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Salary { get; set; }
        public string Role { get; set; }
        public string Account_id { get; set; }
    }
}
