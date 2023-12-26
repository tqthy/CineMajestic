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

        private string id;
        public string ID { get => id; set { id = value; OnPropertyChanged(nameof(ID)); } }

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
        private DateTime endDate;
        public DateTime EndDate { get => endDate; set { endDate = value; OnPropertyChanged(nameof(EndDate)); } }

        private string cost;
        public string Cost { get => cost; set { cost = value; OnPropertyChanged(nameof(Cost)); } }

        private string staffName;
        public string StaffName { get => staffName; set { staffName = value; } }

       

        private ObservableCollection<ErrorDTO> errorList;
        public ObservableCollection<ErrorDTO> ErrorList { get => errorList; set { errorList = value; OnPropertyChanged(nameof(ErrorList)); } }

        // Listview selected item

        private ErrorDTO selectedItem;
        public ErrorDTO SelectedItem { get => selectedItem; set { selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); } }


        // combobox selected item
        private string cbBoxSelectedItem;
        public string CbBoxSelectedItem { get => cbBoxSelectedItem; set { cbBoxSelectedItem = value; OnPropertyChanged(nameof (CbBoxSelectedItem)); } }

        private int comboBoxStatusIndex;
        public int ComboBoxStatusIndex { get => comboBoxStatusIndex; set {comboBoxStatusIndex = value; OnPropertyChanged(nameof(ComboBoxStatusIndex)); } }

        #endregion

        #region ctor
        public ErrorManagementViewModel(int staff_Id) 
        {
            StaffID = staff_Id.ToString();
            IssueDate = DateTime.Now;
            ComboBoxStatusIndex = 1;
            ButtonReportErrorCommand = new ViewModelCommand(ExecuteButtonReportErrorCM);
            ButtonUploadImageCommand = new ViewModelCommand(ExecuteUploadImageCM);
            AddErrorCommand = new ViewModelCommand(ExecuteAddErrorCM, CanExecuteAddErrorCM);
            ButtonEditErrorCommand = new ViewModelCommand(ExecuteButtonEditErrorCM);
            EditErrorCommand = new ViewModelCommand(ExecuteEditErrorCommand, CanExecuteEditErrorCommand);
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
        public ICommand EditErrorCommand { get; set; }

        #endregion

        #region Command Execution

        #region edit error
        public void ExecuteEditErrorCommand(object obj)
        {
            try
            {
                ErrorDA errDA = new();
                if (ComboBoxStatusIndex == 2)
                {
                    errDA.setEndDateAndCost(ID, EndDate, Cost);
                }
                else errDA.setStatus(ID, ComboBoxStatusIndex);
                // cap nhat err list
                List<ErrorDTO> errors = errDA.GetAllErrors();
                ErrorList = new ObservableCollection<ErrorDTO>(errors);
            } catch (Exception ex)
            {

            }
        }
        public bool CanExecuteEditErrorCommand(object obj) 
        {
            if (ComboBoxStatusIndex == 2) return !String.IsNullOrEmpty(Cost);
            return true;
        }
        public void ExecuteButtonEditErrorCM(object obj)
        {
            ID = SelectedItem.Id;
            Cost = SelectedItem.Cost;
            ErrorName = SelectedItem.Name;
            ErrorDescription = SelectedItem.Description;
            StaffDA staffDA = new();
            StaffName = staffDA.Staffstaff_Id(Convert.ToInt32(SelectedItem.Staff_Id)).FullName;
            string applicationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CineMajestic", "ErrorImages", SelectedItem.Image);
            ErrorImage = new BitmapImage(new Uri(applicationFolder));
            if (string.IsNullOrEmpty(SelectedItem.EndDate))
            {
                EndDate = DateTime.Now;
            }
            else EndDate = DateTime.Parse(SelectedItem.EndDate);
            string stt = SelectedItem.Status;
            if (stt == "Chờ tiếp nhận") ComboBoxStatusIndex = 0;
            else if (stt == "Đang xử lý") ComboBoxStatusIndex = 1;
            else if (stt == "Đã xử lý") ComboBoxStatusIndex = 2;
            else if (stt == "Đã huỷ") ComboBoxStatusIndex = 3;
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
