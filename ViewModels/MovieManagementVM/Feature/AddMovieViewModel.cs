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
using System.Xml.Linq;

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
                ValidateTitle();

            }
        }
        private string titleError;
        public string TitleError
        {
            get => titleError;
            set
            {
                titleError = value;
                OnPropertyChanged(nameof(TitleError));
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
                ValidateDescription();

            }
        }

        private string descriptionError;
        public string DescriptionError
        {
            get => descriptionError;
            set
            {
                descriptionError = value;
                OnPropertyChanged(nameof(DescriptionError));
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
                ValidateGenre();
            }
        }
        private string genreError;
        public string GenreError
        {
            get => genreError;
            set
            {
                genreError = value;
                OnPropertyChanged(nameof(GenreError));
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
                ValidateDirector();
            }
        }
        private string directorError;
        public string DirectorError
        {
            get => directorError;
            set
            {
                directorError = value;
                OnPropertyChanged(nameof(DirectorError));
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
                ValidateReleaseYear();
            }
        }

        private string releaseYearError;
        public string ReleaseYearError
        {
            get => releaseYearError;
            set
            {
                releaseYearError = value;
                OnPropertyChanged(nameof(ReleaseYearError));
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
                ValidateLanguage();
            }
        }
        private string languageError;
        public string LanguageError
        {
            get => languageError;
            set
            {
                languageError = value;
                OnPropertyChanged(nameof(LanguageError));
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
                ValidateCountry();
            }
        }

        private string countryError;
        public string CountryError
        {
            get => countryError;
            set
            {
                countryError = value;
                OnPropertyChanged(nameof(CountryError));
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
                ValidateLength();
            }
        }
        private string lengthError;
        public string LengthError
        {
            get => lengthError;
            set
            {
                lengthError = value;
                OnPropertyChanged(nameof(LengthError));
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
                ValidateTrailer();
            }
        }
        private string trailerError;
        public string TrailerError
        {
            get => trailerError;
            set
            {
                trailerError = value;
                OnPropertyChanged(nameof(TrailerError));
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
                OnPropertyChanged(nameof(startDate));
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

        

        //Giá nhập
        private string importPrice;
        public string ImportPrice
        {
            get => importPrice;
            set
            {
                importPrice= value;
                ValidateImportPrice();
            }
        }
        private string importPriceError;
        public string ImportPriceError
        {
            get => importPriceError;
            set
            {
                importPriceError = value;
                OnPropertyChanged(nameof(ImportPriceError));
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
            acceptCommand = new ViewModelCommand(accept,canAcceptAdd);
            addImageCommand = new ViewModelCommand(addImage);
            this.addMovieView = addMovieView;
            StartDate = DateTime.Now;
            Status = "Đang phát hành";
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

        //Phần các hàm validate _ báo lỗi
        private bool[] _canAccept = new bool[10];//phục vụ việc có cho nhấn button accept ko
        private bool canAcceptAdd(object obj)
        {
            return _canAccept[0] && _canAccept[1] && _canAccept[2] && _canAccept[3] && _canAccept[4] && _canAccept[5] &&
                _canAccept[6] && _canAccept[7] && _canAccept[8] && _canAccept[9];
        }
        private void ValidateTitle()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                TitleError = "Không để trống!";
                _canAccept[0] = false;
            }
            else
            {
                TitleError = "";
                _canAccept[0] = true;
            }
        }

        private void ValidateDescription()
        {
            if (string.IsNullOrWhiteSpace(Description))
            {
                DescriptionError = "Không để trống!";
                _canAccept[1] = false;
            }
            else
            {
                DescriptionError = "";
                _canAccept[1] = true;
            }
        }
        private void ValidateGenre()
        {
            if (string.IsNullOrWhiteSpace(Genre))
            {
                GenreError = "Không để trống!";
                _canAccept[2] = false;
            }
            else
            {
                GenreError = "";
                _canAccept[2] = true;
            }
        }

        private void ValidateDirector()
        {
            if (string.IsNullOrWhiteSpace(Director))
            {
                DirectorError = "Không để trống!";
                _canAccept[3] = false;
            }
            else
            {
                DirectorError = "";
                _canAccept[3] = true;
            }
        }

        private void ValidateLength()
        {
            if (string.IsNullOrWhiteSpace(Length))
            {
                LengthError = "Không để trống!";
                _canAccept[4] = false;
            }
            else if (!Length.All(char.IsDigit))
            {
                LengthError = "Không hợp lệ!";
                _canAccept[4] = false;
            }
            else if (int.Parse(Length) < 0)
            {
                LengthError = "Không hợp lệ!";
                _canAccept[4] = false;
            }
            else
            {
                LengthError = "";
                _canAccept[4] = true;
            }
        }

        private void ValidateLanguage()
        {
            if (string.IsNullOrWhiteSpace(Language))
            {
                LanguageError = "Không để trống!";
                _canAccept[5] = false;
            }
            else
            {
                LanguageError = "";
                _canAccept[5] = true;
            }
        }

        private void ValidateCountry()
        {
            if (string.IsNullOrWhiteSpace(Country))
            {
                CountryError = "Không để trống!";
                _canAccept[6] = false;
            }
            else
            {
                CountryError = "";
                _canAccept[6] = true;
            }
        }

        private void ValidateTrailer()
        {
            if (string.IsNullOrWhiteSpace(Trailer))
            {
                TrailerError = "Không để trống!";
                _canAccept[7] = false;
            }
            else
            {
                TrailerError = "";
                _canAccept[7] = true;
            }
        }
        private void ValidateReleaseYear()
        {
            if (string.IsNullOrWhiteSpace(ReleaseYear))
            {
                ReleaseYearError = "Không để trống!";
                _canAccept[8] = false;
            }
            else if (!ReleaseYear.All(char.IsDigit))
            {
                ReleaseYearError = "Không hợp lệ!";
                _canAccept[8] = false;
            }
            else if (int.Parse(ReleaseYear) < 0)
            {
                ReleaseYearError = "Không hợp lệ!";
                _canAccept[8] = false;
            }
            else
            {
                ReleaseYearError = "";
                _canAccept[8] = true;
            }
        }
        private void ValidateImportPrice()
        {
            if (string.IsNullOrWhiteSpace(ImportPrice))
            {
                ImportPriceError = "Không để trống!";
                _canAccept[9] = false;
            }
            else if (!ImportPrice.All(char.IsDigit))
            {
                ImportPriceError = "Không hợp lệ!";
                _canAccept[9] = false;
            }
            else if (int.Parse(ImportPrice) < 0)
            {
                ImportPriceError = "Không hợp lệ!";
                _canAccept[9] = false;
            }
            else
            {
                ImportPriceError = "";
                _canAccept[9] = true;
            }
        }



    }
}
