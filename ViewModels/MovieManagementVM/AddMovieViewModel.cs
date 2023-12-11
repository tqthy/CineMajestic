using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CineMajestic.ViewModels.MovieManagementVM
{
    public partial class MovieManagementViewModel
    {
        public ICommand AddMovieCommand { get; }

        public void ExecuteAddMovieCommand(object parameter)
        {
            MovieDTO movie = new MovieDTO();
            movie.Title = Title;
            movie.Director = Director;
            movie.Country = Country;
            movie.Length = Length;
            MovieDA movieDA = new MovieDA();
            movieDA.AddMovie(movie);
            MovieList = new ObservableCollection<MovieDTO>(movieDA.GetAllMovies());
        }
        public bool CanExecuteAddMovieCommand(object paramenter)
        {
            return (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Director) && !string.IsNullOrEmpty(Country) && !string.IsNullOrEmpty(Length));
        }
    }
}
