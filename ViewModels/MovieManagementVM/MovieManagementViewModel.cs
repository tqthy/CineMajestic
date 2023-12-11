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

namespace CineMajestic.ViewModels.MovieManagementVM
{
    public partial class MovieManagementViewModel : MainBaseViewModel
    {
        private string title;
        public string Title { get => title; set { title = value; OnPropertyChanged(nameof(Title)); } }
        private string country;
        public string Country { get => country; set { country = value; OnPropertyChanged(nameof(Country)); } }
        private string length;
        public string Length { get => length; set { length = value; OnPropertyChanged(nameof(Length)); } }
        private string director;
        public string Director { get => director; set { director = value; OnPropertyChanged(nameof(Director)); } }

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

        // Commands
        public ICommand ButtonAddMovieCommand { get; }
        public ICommand RemoveMovieCommand { get; }
        public ICommand ButtonEditMovieCommand { get; }
        // Contructor
        public MovieManagementViewModel() 
        {
            MovieDA movieDA = new MovieDA();
            MovieList = new ObservableCollection<MovieDTO>(movieDA.GetAllMovies());
            AddMovieCommand = new ViewModelCommand(ExecuteAddMovieCommand, CanExecuteAddMovieCommand);
            ButtonAddMovieCommand = new ViewModelCommand(ExecuteButtonAddMovieCommand);
        }
        // Commands execution
        public void ExecuteButtonAddMovieCommand(object obj)
        {
            AddMovieView addMoviePopup = new AddMovieView();
            addMoviePopup.DataContext = this;
            addMoviePopup.ShowDialog();
        }
    }
}
