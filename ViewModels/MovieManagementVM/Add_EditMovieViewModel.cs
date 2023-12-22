using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        public ICommand AddOrEditMovieCommand { get; }
        public ICommand UploadPosterCommand { get; }

        #endregion

        #region Commands execution

        // Add movie
        public void ExecuteAddOrEditMovieCommand(object parameter)
        {
            MovieDTO movie = new MovieDTO();
            movie.Title = Title;
            movie.Director = Director;
            movie.Country = Country;
            movie.Length = Length;
            movie.ReleaseYear = ReleaseYear;
            movie.Description = Description;
            movie.StartDate = StartDate;
            movie.EndDate = EndDate;
            movie.Language = Language;
            movie.Trailer = Trailer;
            movie.Genre = SelectedGenre.Id;
            movie.Poster = Path.GetFileName(MoviePoster.UriSource.ToString());
            string applicationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CineMajestic", Title, "Poster");
            if (!Directory.Exists(applicationFolder))
            {
                Directory.CreateDirectory(applicationFolder);
            }
            File.Copy(MoviePoster.UriSource.LocalPath, Path.Combine(applicationFolder, movie.Poster), true);
            MovieDA movieDA = new MovieDA();
            if (WindowTitle == "Thêm phim") movieDA.AddMovie(movie);
            else movieDA.EditMovie(movie);

            MovieList = new ObservableCollection<MovieDTO>(movieDA.GetAllMovies());

            MessageBox.Show("Success");
        }
        public bool CanExecuteAddOrEditMovieCommand(object parameter)
        {
            return (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Director) && !string.IsNullOrEmpty(Country) && !string.IsNullOrEmpty(Length) && !string.IsNullOrEmpty(ReleaseYear) &&
                    !string.IsNullOrEmpty(Description) && !string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate) && !string.IsNullOrEmpty(Language) && !string.IsNullOrEmpty(Trailer) &&
                    SelectedGenre != null && MoviePoster != null);
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
