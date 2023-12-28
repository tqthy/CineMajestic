using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DTOs.Bills
{
    public class BillDTO
    {
        public int Id { get; set; }
        public string BillDate { get; set; }
        public int Total { get; set; }
    }
}
