using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.ViewModels.MovieManagementVM
{
    public partial class MovieManagementViewModel:MainBaseViewModel
    {
        private ObservableCollection<MovieDTO> DSPhim_All;
        private ObservableCollection<MovieDTO> DSPhim_DPH;//đang phát hành
        private ObservableCollection<MovieDTO> DSPhim_NPH;//ngưng phát hành


        public MovieManagementViewModel()
        {
            AddMovie();
            // SearchMovie(); gọi ở loaddata r
            Delete();
            MovieDetail();
            Filter();
        }

    }
}
