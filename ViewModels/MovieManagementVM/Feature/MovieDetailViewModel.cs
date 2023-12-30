using CineMajestic.Models.DTOs;
using CineMajestic.Views.MovieManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CineMajestic.ViewModels.MovieManagementVM
{
    public partial class MovieManagementViewModel
    {
        public ICommand MovieDetailCommand {  get; set; }
        private void MovieDetail()
        {
            MovieDetailCommand = new ViewModelCommand(movieDetail);
        }

        private void movieDetail(object obj)
        {
            if (obj != null)
            {
                MovieDetailView movieDetailView = new MovieDetailView(obj as MovieDTO);
                movieDetailView.ShowDialog();
            }
        }
    }


    public class MovieDetailViewModel:MainBaseViewModel
    {
        private MovieDTO movieDTO;
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
                description = value;
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
                genre = value;
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
                director = value;
                OnPropertyChanged(nameof(Director));
            }
        }


        //năm phát hành
        private string releaseYear;
        public string ReleaseYear
        {
            get => releaseYear;
            set
            {
                releaseYear = value;
                OnPropertyChanged(nameof(ReleaseYear));
            }
        }


        //ngôn ngữ
        private string language;
        public string Language
        {
            get => language;
            set
            {
                language = value;
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
                length = value;
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
                trailer = value;
                OnPropertyChanged(nameof(Trailer));
            }
        }


        //ngày bắt đầu
        private string startDate;
        public string StartDate
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
                status = value;
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

        private string importprice;
        public string ImportPrice
        {
            get => importprice;
            set
            {
                importprice = value;
                OnPropertyChanged(nameof(ImportPrice));
            }
        }
        public MovieDetailViewModel(MovieDTO movieDTO)
        {
            this.movieDTO = movieDTO;
            loadMovie();
        }


        private void loadMovie()
        {
            Title=movieDTO.Title;
            Description=movieDTO.Description;
            Genre=movieDTO.Genre;
            Director= movieDTO.Director;
            ReleaseYear=movieDTO.ReleaseYear;
            Language=movieDTO.Language;
            Country=movieDTO.Country;
            Length = movieDTO.Length.ToString();
            Trailer= movieDTO.Trailer;
            StartDate= movieDTO.StartDate;
            Status=movieDTO.Status;
            ImportPrice = movieDTO.ImportPrice.ToString();
            ImageSource=movieDTO.ImageSource;
        }
    }
}
