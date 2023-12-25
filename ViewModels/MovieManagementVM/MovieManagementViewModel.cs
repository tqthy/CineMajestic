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
        private ObservableCollection<MovieDTO> dSPhim;
        public ObservableCollection<MovieDTO> DSPhim
        {
            get=> dSPhim;
            set
            {
                dSPhim = value;
                OnPropertyChanged(nameof(DSPhim));
            }
        }


        public MovieManagementViewModel()
        {
            AddMovie();
            // SearchMovie(); gọi ở loaddata r
            loadData();

        }


        private void loadData()
        {
            MovieDA movieDA = new MovieDA();
            DSPhim = movieDA.getAllMovie();
            SearchMovie();
        }
    }
}
