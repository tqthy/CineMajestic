﻿using CineMajestic.Models.DataAccessLayer;
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

        // Genres List

        private ObservableCollection<GenreDTO> genreList;
        public ObservableCollection<GenreDTO> GenreList
        {
            get => genreList;
            set
            {
                genreList = value;
                OnPropertyChanged(nameof(GenreList));
            }
        }

        // ComboBox selected genre
        private GenreDTO selectedGenre;
        public GenreDTO SelectedGenre { get => selectedGenre; set { selectedGenre = value; OnPropertyChanged(nameof(SelectedGenre)); } }

        // Listview selected item

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
            _ = LoadCollection();
            AddMovieCommand = new ViewModelCommand(ExecuteAddMovieCommand, CanExecuteAddMovieCommand);
            ButtonAddMovieCommand = new ViewModelCommand(ExecuteButtonAddMovieCommand);
            UploadPosterCommand = new ViewModelCommand(ExecuteUploadPosterCommand);
        }

        private async Task LoadCollection()
        {
            MovieDA movieDA = new MovieDA();
            GenreDA genreDA = new GenreDA();

            Task<List<MovieDTO>> movietasks = Task.Run(() => movieDA.GetAllMovies());
            Task<List<GenreDTO>> genretasks = Task.Run(() => genreDA.GetAllGenres());

            List<MovieDTO> movies = await movietasks;
            List<GenreDTO> genres = await genretasks;

            MovieList = new ObservableCollection<MovieDTO>(movies);
            GenreList = new ObservableCollection<GenreDTO>(genres);
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
