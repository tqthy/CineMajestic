using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using CineMajestic.Views.MovieManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.MovieManagementVM
{
    public partial class MovieManagementViewModel
    {
        public ICommand addMovieCommand { get; set; }

        private void AddMovie()
        {
            addMovieCommand = new ViewModelCommand(addMovie);
        }
        private void addMovie(object obj)
        {
            AddMovieView addMovieView = new AddMovieView();
            addMovieView.ShowDialog();

            loadData();
        }
    }


    public class AddMovieViewModel : MainBaseViewModel
    {
        //tiêu đề
        private string title;
        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }


        //chi tiết
        private string description;
        public string Description
        {
            get => description;
            set
            {
                description= value;
                OnPropertyChanged(nameof(Description));
            }
        }


        //thể loại
        private string genre;
        public string Genre
        {
            get => genre;
            set
            {
                genre= value;
                OnPropertyChanged(nameof(Genre));
            }
        }

        //đạo diễn
        private string director;
        public string Director
        {
            get => director;
            set
            {
                director= value;
                OnPropertyChanged(nameof(Director));
            }
        }


        //năm phát hành
        private DateTime? releaseYear;
        public DateTime? ReleaseYear
        {
            get => releaseYear;
            set
            {
                releaseYear = value;
                OnPropertyChanged(nameof (ReleaseYear));
            }
        }


        //ngôn ngữ
        private string language;
        public string Language
        {
            get => language;
            set
            {
                language= value;
                OnPropertyChanged(nameof(Language));
            }
        }


        //quốc gia
        private string country;
        public string Country
        {
            get => country;
            set
            {
                country = value;
                OnPropertyChanged(nameof(Country));
            }
        }


        //thời lượng
        //dùng string để nao xử lý lỗi người dùng
        private string length;
        public string Length
        {
            get => length;
            set
            {
                length= value;
                OnPropertyChanged(nameof(Length));
            }
        }


        //trailer
        private string trailer;
        public string Trailer
        {
            get => trailer;
            set
            {
                trailer= value;
                OnPropertyChanged(nameof(Trailer));
            }
        }


        //ngày bắt đầu
        private DateTime? startDate;
        public DateTime? StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }


        //trạng thái
        private string status;
        public string Status
        {
            get => status;
            set
            {
                status= value;
                OnPropertyChanged(nameof(Status));
            }
        }

        //poster
        private string imageSource;
        public string? ImageSource
        {
            get => imageSource;
            set
            {
                if (imageSource != value)
                {
                    imageSource = value;
                    OnPropertyChanged(nameof(ImageSource));
                }
            }
        }



        private AddMovieView addMovieView;
        public ICommand acceptCommand {  get; set; }
        public ICommand quitCommand {  get; set; }

        public AddMovieViewModel(AddMovieView addMovieView)
        {
            this.addMovieView= addMovieView;
            acceptCommand = new ViewModelCommand(accept);
            quitCommand=new ViewModelCommand(quit);
        }
        private void accept(object obj)
        {
            MovieDA movieDA = new MovieDA();
            movieDA.addMovie(new MovieDTO(Title, Description, Director, ReleaseYear.Value.ToString("yyyy-MM-dd"), Language, Country, int.Parse(Length), Trailer, StartDate.Value.ToString("yyyy-MM-dd"), Genre, Status, ImageSource));
            MessageBox.Show("Thêm thành công!");
        }

        private void quit(object obj)
        {
            addMovieView.Close();
        }
    }
}
