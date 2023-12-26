using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.ViewModels.ShowTimeManagementVM
{
    public partial class ShowTimeManagementViewModel : MainBaseViewModel
    {
        #region Bindable Property

        private ObservableCollection<ShowTimeDTO> screeningList;
        public ObservableCollection<ShowTimeDTO> ScreeningList {  get { return screeningList; } set { screeningList = value; OnPropertyChanged(nameof(ScreeningList)); } }

        #endregion



        #region Constructor

        public ShowTimeManagementViewModel()
        { 
            ShowTimeDA showTimeDA = new ShowTimeDA();
            List<ShowTimeDTO> showTimeDTOs = showTimeDA.GetAllShowTimeByDate(DateTime.Now);
            ScreeningList = new ObservableCollection<ShowTimeDTO>(showTimeDTOs);
        }

        #endregion



        #region Commands



        #endregion
    }
}
