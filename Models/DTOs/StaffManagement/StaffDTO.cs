using CineMajestic.Models.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DTOs.StaffManagement
{
    public class StaffDTO
    {
        public int Id { get; set; }
        public string IdFormat {  get; set; }
        public string FullName { get; set; }
        public string Birth { get; set; }
        public string Gender {  get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Salary { get; set; }
        public string Role { get; set; }
        public string NgayVaoLam {  get; set; }
        public string ImageSource {  get; set; }

        //phục vụ edit
        public StaffDTO(int id,string fullName,string birth,string gender,string email,string phoneNumber,int salary,string role,string ngayVL)
        {
            Id=id;
            FullName=fullName;
            Birth=birth;
            Gender=gender;
            Email=email;
            PhoneNumber=phoneNumber;
            Salary=salary;
            Role=role;
            NgayVaoLam=ngayVL;
            IdFormat = StaffDA.formatID(Id);
        }


        //phục vụ việc thêm 1 staff
        public StaffDTO(string fullName, string birth, string gender, string email, string phoneNumber, int salary, string role, string ngayVL)
        {
            FullName = fullName;
            Birth = birth;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            Salary = salary;
            Role = role;
            NgayVaoLam = ngayVL;
        }


        //phục vụ phần information và lấy data
        public StaffDTO(int id, string fullName, string birth, string gender, string email, string phoneNumber, int salary, string role, string ngayVL,string imageSource)
        {
            Id = id;
            FullName = fullName;
            Birth = birth;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            Salary = salary;
            Role = role;
            NgayVaoLam = ngayVL;
            IdFormat = StaffDA.formatID(Id);
            ImageSource = imageSource;
        }
    }
}
