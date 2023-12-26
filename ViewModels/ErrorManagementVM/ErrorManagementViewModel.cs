using CineMajestic.Views.ErrorManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        #endregion

        #region ctor
        public ErrorManagementViewModel(int staff_Id) 
        {
            StaffID = staff_Id.ToString();
            IssueDate = DateTime.Now;
            ButtonReportErrorCommand = new ViewModelCommand(ExecuteButtonReportErrorCM);
        }

        #endregion

        #region Command

        public ICommand ButtonReportErrorCommand { get; set; }

        #endregion

        #region Command Execution

        public void ExecuteButtonReportErrorCM(object obj)
        {
            ErrorReportView popup = new();
            popup.DataContext = this;
            popup.ShowDialog();
        }

        #endregion

        #region others

        #endregion
    }
}
