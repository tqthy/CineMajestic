using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using CineMajestic.Views.MovieManagement;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.MovieManagementVM
{
    public partial class MovieManagementViewModel
    {
        
        public ICommand EditMovieCommand { get; set; }

        private void edit()
        {
            EditMovieCommand = new ViewModelCommand(EditMovie);
        }

        private void EditMovie(object obj)
        {
            EditFilmView editFilmView = new EditFilmView(obj as MovieDTO);
            editFilmView.ShowDialog();

            loadData();
        }
    }


    public class EditMovieViewModel : MainBaseViewModel
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


        public ICommand acceptCommand { get; set; }
        public ICommand addImageCommand {  get; set; }

        private EditFilmView editFilmView;
        public EditMovieViewModel(MovieDTO movieDTO, EditFilmView editFilmView)
        {
            this.movieDTO = movieDTO;
            loadMovieCurrent();
            acceptCommand = new ViewModelCommand(accept);
            addImageCommand = new ViewModelCommand(addImage);
            this.editFilmView = editFilmView;
        }

        private void loadMovieCurrent()
        {
            Title = movieDTO.Title;
            Description = movieDTO.Description;
            Genre = movieDTO.Genre;
            Director = movieDTO.Director;
            ReleaseYear = movieDTO.ReleaseYear;
            Language = movieDTO.Language;
            Country = movieDTO.Country;
            Length = movieDTO.Length.ToString();
            Trailer = movieDTO.Trailer;


            string[]startDate= movieDTO.StartDate.Split('/');
            StartDate= new DateTime(int.Parse(startDate[2]), int.Parse(startDate[1]), int.Parse(startDate[0]));

            Status = movieDTO.Status;
            ImportPrice = movieDTO.ImportPrice.ToString();
            ImageSource = movieDTO.ImageSource;
        }

        private void accept(object obj)
        {
            string imageSource = "";
            try
            {
                imageSource = Path.GetFileName(ImageSource);
            }
            catch { }


            MovieDA movieDA = new MovieDA();

            try
            {
                movieDA.editMovie(new MovieDTO(movieDTO.Id, Title, Description, Director, ReleaseYear, Language, Country, int.Parse(Length), Trailer, StartDate.Value.ToString("yyyy-MM-dd"), Genre, Status, imageSource, int.Parse(ImportPrice)));
            }
            catch { }

            //cập nhật lại billAddMovie chỗ này: khỏi nha vì tạo trigger roài
            MessageBox.Show("Sửa thành công!");

            editFilmView.Close();
        }


        //add image
        private void addImage(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Ảnh (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                try
                {
                    string selectedImagePath = openFileDialog.FileName;
                    string folder = Path.GetDirectoryName(selectedImagePath);
                    string fileName = Path.GetFileName(selectedImagePath);
                    string extension = Path.GetExtension(fileName);//đuôi mở rộng của file


                    string fileOld1 = selectedImagePath;

                    string pathfileOld2 = selectedImagePath;

                    while (File.Exists(MotSoPTBoTro.pathProject() + @"Images\MovieManagement\" + fileName))
                    {
                        fileName = MotSoPTBoTro.RandomFileName() + extension;
                        File.Move(fileOld1, folder + @"\" + fileName);
                        pathfileOld2 = folder + @"\" + fileName;
                    }

                    try
                    {
                        MotSoPTBoTro.copyFile(pathfileOld2, MotSoPTBoTro.pathProject() + @"Images\MovieManagement");
                        ImageSource = MotSoPTBoTro.pathProject() + @"Images\MovieManagement\" + fileName;


                        //đổi lại tên file người dùng chọn
                        File.Move(pathfileOld2, selectedImagePath);
                    }
                    catch { }


                }
                catch { }
            }
        }
    }
}
