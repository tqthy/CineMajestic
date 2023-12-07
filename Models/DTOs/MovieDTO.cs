using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DTOs
{
    public class MovieDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public string ReleaseDate { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Length { get; set; }
        public string Trailer { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public IEnumerable<GenreDTO> Genres { get; set; }
    }
}
