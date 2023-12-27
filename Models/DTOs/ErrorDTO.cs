using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CineMajestic.Models.DTOs
{
    public class ErrorDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Staff_Id { get; set; }
        public string DateAdded { get; set; }
        public string Status { get; set; }
        public string EndDate { get; set; }
        public string Cost { get; set; }
        public string Image { get; set; }

        public System.Windows.Media.Brush StatusColor { get; set; }
    }
}
