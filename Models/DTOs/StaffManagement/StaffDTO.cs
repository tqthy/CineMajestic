﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DTOs.StaffManagement
{
    public class StaffDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Birth { get; set; }
        public string Gender {  get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Salary { get; set; }
        public int Role { get; set; }//1 là quản lý ,2 là nhân viên

        public StaffDTO(int id,string fullName,string birth,string gender,string email,string phoneNumber,int salary,int role)
        {
            Id=id;
            FullName=fullName;
            Birth=birth;
            Gender=gender;
            Email=email;
            PhoneNumber=phoneNumber;
            Salary=salary;
            Role=role;
        }
    }
}
