using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.ViewModels
{
    public class MovieManagementViewModel : MainBaseViewModel
    {
        private string title;
        public string Title { get => title; set { title = value; OnPropertyChanged(nameof(Title)); } }
        private string country;
        public string Country { get => title; set { title = value; OnPropertyChanged(nameof(Title)); } }
        private string length;
        public string Length { get => title; set { title = value; OnPropertyChanged(nameof(Title)); } }
        private string director;
        public string Director { get => title; set { title = value; OnPropertyChanged(nameof(Title)); } }

        private ObservableCollection<MovieDTO> movieList;
        public ObservableCollection<MovieDTO> MovieList
        {
            get => movieList;
            set
            {
                movieList = value;
                OnPropertyChanged(nameof(MovieList));
            }
        }

        public MovieManagementViewModel() 
        {
            MovieDA movieDA = new MovieDA();
            MovieList = new ObservableCollection<MovieDTO>(movieDA.GetAllMovies());
        }
    }
}
