using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs.ShowTimeManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CineMajestic.ViewModels.ShowTimeManagementVM
{
    public partial class TicketBookingViewModel
    {

        private ObservableCollection<SeatForShowTimeDTO> dSGhe;
        public ObservableCollection<SeatForShowTimeDTO> DSGhe
        {
            get => dSGhe;
            set
            {
                dSGhe = value;
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
            SeatForShowTimeDA seatForShowTimeDA=new SeatForShowTimeDA();
            DSGhe = seatForShowTimeDA.getDSGhe(showTimeDTO.Id);
        }


        private void selectedSeat(object obj)
        {
            if (obj is SeatForShowTimeDTO seatForShowTimeDTO)
            {
                if(Seats== "Hiện chưa chọn ghế nào!")
                {
                    Seats = "";
                }
                seatForShowTimeDTO.Condition = !seatForShowTimeDTO.Condition;

                if (seatForShowTimeDTO.Condition)
                {
                    if (Seats != "")
                    {
                        Seats += "," + seatForShowTimeDTO.Location;
                    }
                    else
                    {
                        Seats = seatForShowTimeDTO.Location;
                    }
                    TotalPriceTicket += showTimeDTO.PerSeatTicketPrice;
                }
                else
                {
                    TotalPriceTicket-=showTimeDTO.PerSeatTicketPrice;

                    if(Seats.Contains(','))
                    {
                        string[]kq=Seats.Split(',');
                        Seats = "";
                        foreach(string item in kq)
                        {
                            if (item != seatForShowTimeDTO.Location)
                            {
                                if (Seats != "")
                                {
                                    Seats += "," + item;
                                }
                                else
                                {
                                    Seats += item;
                                }
                            }
                        }
                    }
                    else
                    {
                        Seats = "";
                    }
                }
            }
        }
    }
}
