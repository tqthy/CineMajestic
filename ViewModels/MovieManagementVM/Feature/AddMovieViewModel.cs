using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using CineMajestic.Views.MovieManagement;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        private string releaseYear;
        public string ReleaseYear
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

        private string importPrice;
        public string ImportPrice
        {
            get => importPrice;
            set
            {
                importPrice= value;
                OnPropertyChanged(nameof(ImportPrice));
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


        public ICommand acceptCommand {  get; set; }
        public ICommand addImageCommand {  get; set; }


        private AddMovieView addMovieView;
        public AddMovieViewModel(AddMovieView addMovieView)
        {
            acceptCommand = new ViewModelCommand(accept);
            addImageCommand = new ViewModelCommand(addImage);
            this.addMovieView = addMovieView;
        }
        private void accept(object obj)
        {
            MovieDA movieDA = new MovieDA();
            movieDA.addMovie(new MovieDTO(Title, Description, Director, ReleaseYear, Language, Country, int.Parse(Length), Trailer, StartDate.Value.ToString("yyyy-MM-dd"), Genre, Status, Path.GetFileName(ImageSource), int.Parse(ImportPrice)));

            //lấy ngày tháng năm hiện tại
            DateTime dateTime = DateTime.Now;

            //thêm vào bill addmovie
            BillAddMovieDA billAddMovieDA = new BillAddMovieDA();
            billAddMovieDA.addBill(new BillAddMovieDTO(movieDA.identCurrent(), dateTime.ToString("yyyy-MM-dd"), int.Parse(ImportPrice)));
            MessageBox.Show("Thêm thành công!");
            addMovieView.Close();
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
