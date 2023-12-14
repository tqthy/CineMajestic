using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace CineMajestic.ViewModels.MovieManagementVM
{
    public partial class MovieManagementViewModel
    {

        #region Commands
        public ICommand AddMovieCommand { get; }
        public ICommand UploadPosterCommand { get; }
        #endregion

        #region Commands execution

        // Add movie
        public void ExecuteAddMovieCommand(object parameter)
        {
            MovieDTO movie = new MovieDTO();
            movie.Title = Title;
            movie.Director = Director;
            movie.Country = Country;
            movie.Length = Length;
            movie.Description = Description;
            movie.StartDate = StartDate;
            movie.EndDate = EndDate;
            movie.Language = Language;
            movie.Genre = Genre;
            movie.Poster = MoviePoster.UriSource.ToString();
            MovieDA movieDA = new MovieDA();
            movieDA.AddMovie(movie);
            MovieList = new ObservableCollection<MovieDTO>(movieDA.GetAllMovies());
        }
        public bool CanExecuteAddMovieCommand(object parameter)
        {
            return (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Director) && !string.IsNullOrEmpty(Country) && !string.IsNullOrEmpty(Length));
        }

        // Upload Poster

        public void ExecuteUploadPosterCommand(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog.Title = "Hãy chọn file ảnh poster cho phim.";
            if (openFileDialog.ShowDialog() == true)
            {
                MoviePoster = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }


        #endregion
    }
}
