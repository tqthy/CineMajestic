using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DTOs
{
    public class MovieStatisticsDTO
    {
        public string Rank { get; set; }
        public string Title { get; set; }
        public string ViewCount { get; set; }
        public string Income { get; set; }
    }
}
