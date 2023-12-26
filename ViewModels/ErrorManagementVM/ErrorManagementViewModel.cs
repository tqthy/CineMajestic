using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using CineMajestic.Views.ErrorManagement;
using Microsoft.VisualBasic.Devices;
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

namespace CineMajestic.ViewModels.ErrorManagementVM
{
    public partial class ErrorManagementViewModel : MainBaseViewModel
    {
        #region Bindable property

        private string errorName;
        public string ErrorName { get => errorName; set { errorName = value; OnPropertyChanged(nameof(ErrorName)); } }
        private string errorDescription;
        public string ErrorDescription { get => errorDescription; set {errorDescription = value; OnPropertyChanged(nameof(ErrorDescription)); } }
        private string staffID;
        public string StaffID { get => staffID; set {staffID = value; OnPropertyChanged(nameof(StaffID)); } }
        private DateTime issueDate;
        public DateTime IssueDate { get => issueDate; set {issueDate = value; OnPropertyChanged(nameof(IssueDate)); } }
        private BitmapImage errorImage;
        public BitmapImage ErrorImage { get => errorImage; set { errorImage = value; OnPropertyChanged(nameof(ErrorImage)); } }



        private ObservableCollection<ErrorDTO> errorList;
        public ObservableCollection<ErrorDTO> ErrorList { get => errorList; set { errorList = value; OnPropertyChanged(nameof(ErrorList)); } }

        // Listview selected item

        private ErrorDTO selectedItem;
        public ErrorDTO SelectedItem { get => selectedItem; set { selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); } }
    
        #endregion

        #region ctor
        public ErrorManagementViewModel(int staff_Id) 
        {
            StaffID = staff_Id.ToString();
            IssueDate = DateTime.Now;
            ButtonReportErrorCommand = new ViewModelCommand(ExecuteButtonReportErrorCM);
            ButtonUploadImageCommand = new ViewModelCommand(ExecuteUploadImageCM);
            AddErrorCommand = new ViewModelCommand(ExecuteAddErrorCM, CanExecuteAddErrorCM);
            ErrorDA errDA = new();
            List<ErrorDTO> errors = errDA.GetAllErrors();
            ErrorList = new ObservableCollection<ErrorDTO>(errors);
        }

        #endregion

        #region Command

        public ICommand ButtonReportErrorCommand { get; set; }
        public ICommand ButtonUploadImageCommand { get; set; }
        public ICommand AddErrorCommand { get; set; }
        public ICommand ButtonEditErrorCommand { get; set; }

        #endregion

        #region Command Execution

        #region edit error
        public void ExecuteButtonEditErrorCM(object obj)
        {
            ErrorName = SelectedItem.Name;
            ErrorDescription = SelectedItem.Description;
            StaffID = SelectedItem.Staff_Id;
            IssueDate = Convert.ToDateTime(SelectedItem.DateAdded);
            string applicationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CineMajestic", "ErrorImages", SelectedItem.Image);
            ErrorImage = new BitmapImage(new Uri(applicationFolder));

            ErrorEditView popup = new();
            popup.DataContext = this;
            popup.ShowDialog();
        }
        #endregion

        #region report error

        public void ExecuteButtonReportErrorCM(object obj)
        {
            ErrorReportView popup = new();
            popup.DataContext = this;
            popup.ShowDialog();
        }

        public void ExecuteUploadImageCM(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog.Title = "Hãy chọn file ảnh sự cố";
            if (openFileDialog.ShowDialog() == true)
            {
                ErrorImage = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        public void ExecuteAddErrorCM(object obj)
        {
            ErrorDA errorDA = new ErrorDA();
            ErrorDTO errorDTO = new ErrorDTO() { 
                Name = ErrorName,
                Description = ErrorDescription,
                Staff_Id = StaffID,
                DateAdded = IssueDate.ToString(),
                Image = Path.GetFileName(ErrorImage.UriSource.ToString())
            };
            try
            {
                errorDA.UploadError(errorDTO);
            }
            catch (Exception ex)
            {
                return;
            }
            string applicationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CineMajestic", "ErrorImages");
            if (!Directory.Exists(applicationFolder))
            {
                Directory.CreateDirectory(applicationFolder);
            }
            File.Copy(ErrorImage.UriSource.LocalPath, Path.Combine(applicationFolder, errorDTO.Image), true);

            // MSG BOX CUSTOM
            MessageBox.Show("Add thanh cong!");
        }

        public bool CanExecuteAddErrorCM(object obj)
        {
            return true;
        }

        #endregion

        #endregion

        #region others

        #endregion
    }
}
