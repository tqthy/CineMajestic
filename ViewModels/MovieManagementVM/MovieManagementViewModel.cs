using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using CineMajestic.Views.MovieManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CineMajestic.ViewModels.MovieManagementVM
{
    public partial class MovieManagementViewModel : MainBaseViewModel
    {
        #region Binding properties

        private string id;
        public string Id { get => id; set { id = value; OnPropertyChanged(nameof(Id)); } }
        private string title;
        public string Title { get => title; set { title = value; OnPropertyChanged(nameof(Title)); } }
        private string country;
        public string Country { get => country; set { country = value; OnPropertyChanged(nameof(Country)); } }
        private string length;
        public string Length { get => length; set { length = value; OnPropertyChanged(nameof(Length)); } }
        private string description;
        public string Description { get => description; set { description = value; OnPropertyChanged(nameof(Description)); } }
        private string director;
        public string Director { get => director; set { director = value; OnPropertyChanged(nameof(Director)); } }
        private string releaseYear;
        public string ReleaseYear { get => releaseYear; set { releaseYear = value; OnPropertyChanged(nameof(ReleaseYear)); } }
        private string trailer;
        public string Trailer { get => trailer; set { trailer = value; OnPropertyChanged(nameof(Trailer)); } }
        private string language;
        public string Language { get => language; set { language = value; OnPropertyChanged(nameof(Language)); } }
        private string startDate;
        public string StartDate { get => startDate; set { startDate = value; OnPropertyChanged(nameof(StartDate)); } }
        private string endDate;
        public string EndDate { get => endDate; set { endDate = value; OnPropertyChanged(nameof(EndDate)); } }
        private string genre;
        public string Genre { get => genre; set { genre = value; OnPropertyChanged(nameof(Genre)); } }
        private BitmapImage moviePoster;
        public BitmapImage MoviePoster { get => moviePoster; set { moviePoster = value; OnPropertyChanged(nameof(MoviePoster)); } }
        
        // Movies List

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

        private MovieDTO selectedItem;
        public MovieDTO SelectedItem { get => selectedItem; set { selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); } }

        #endregion



        #region Commands

        public ICommand ButtonAddMovieCommand { get; }
        public ICommand RemoveMovieCommand { get; }
        public ICommand ButtonEditMovieCommand { get; }

        #endregion


        #region Constructor
        public MovieManagementViewModel()
        {
            MovieDA movieDA = new MovieDA();
            MovieList = new ObservableCollection<MovieDTO>(movieDA.GetAllMovies());
            AddMovieCommand = new ViewModelCommand(ExecuteAddMovieCommand, CanExecuteAddMovieCommand);
            ButtonAddMovieCommand = new ViewModelCommand(ExecuteButtonAddMovieCommand);
            UploadPosterCommand = new ViewModelCommand(ExecuteUploadPosterCommand);
        }

        #endregion

        #region Commands execution

        public void ExecuteButtonAddMovieCommand(object obj)
        {
            AddMovieView addMoviePopup = new AddMovieView();
            MoviePoster = null;
            addMoviePopup.DataContext = this;
            addMoviePopup.ShowDialog();
        }

        #endregion

    }
}
