using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.ViewModels
{
    public class MovieManagementViewModel : MainBaseViewModel
    {
        private string title;
        public string Title { get => title; set { title = value; OnPropertyChanged(nameof(Title)); } }

        public MovieManagementViewModel() 
        {
            
        }
    }
}
