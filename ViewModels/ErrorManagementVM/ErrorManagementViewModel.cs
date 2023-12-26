using CineMajestic.Views.ErrorManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CineMajestic.ViewModels.ErrorManagementVM
{
    public partial class ErrorManagementViewModel : MainBaseViewModel
    {
        #region Bindable property

        #endregion

        #region ctor
        public ErrorManagementViewModel() 
        {
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
            popup.ShowDialog();
        }

        #endregion

        #region others

        #endregion
    }
}
