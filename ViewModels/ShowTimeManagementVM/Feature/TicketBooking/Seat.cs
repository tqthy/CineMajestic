using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs.ShowTimeManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.ShowTimeManagementVM
{
    public partial class TicketBookingViewModel
    {
        private ObservableCollection<SeatDTO> dSGhe;
        public ObservableCollection<SeatDTO> DSGhe
        {
            get => dSGhe;
            set
            {
                dSGhe= value;
                OnPropertyChanged(nameof(DSGhe));
            }
        }

        public ICommand SelectedSeatCommand {  get; set; }

        private void Seat()
        {
            loadSeat();
            SelectedSeatCommand = new ViewModelCommand(selectedSeat);
        }
        private void loadSeat()
        {
            SeatDA seatDA = new SeatDA();
            DSGhe=seatDA.getDSGhe(showTimeDTO.Auditorium_Id);
        }

        private void selectedSeat(object obj)
        {
            if(obj is SeatDTO seatDTO)
            {
                seatDTO.Condition = !seatDTO.Condition;
            }
        }
    }
}
