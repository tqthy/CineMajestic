using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DTOs
{
    public class UserDTO
    {
        public string Id {  get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AccountType { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Salary {  get; set; }
    }
}
