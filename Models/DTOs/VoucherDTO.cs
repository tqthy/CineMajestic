using Microsoft.Xaml.Behaviors.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DTOs
{
    public class VoucherDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string VoucherDetail {  get; set; }
        public string Type {  get; set; }
        public string StartDate {  get; set; }
        public string FinDate {  get; set; }

        public VoucherDTO()
        {

        }

        public VoucherDTO(int id, string name, string code, string voucherDetail, string type, string startDate, string finDate)
        {
            Id = id;
            Name = name;
            Code = code;
            VoucherDetail = voucherDetail;
            Type = type;
            StartDate = startDate;
            FinDate = finDate;
        }

        public VoucherDTO(string name, string code, string voucherDetail, string type, string startDate, string finDate)
        {
            Name = name;
            Code = code;
            VoucherDetail = voucherDetail;
            Type = type;
            StartDate = startDate;
            FinDate = finDate;
        }

    }
}
