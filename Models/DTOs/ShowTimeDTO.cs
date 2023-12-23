using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DTOs
{
    public class ShowTimeDTO
    {
        public string? Id { get; set; }
        public string? Movie_Id { get; set; }
        public string? Auditorium_Id { get; set; }
        public ICollection<DateTime>? StartTime { get; set; }
    }
}
