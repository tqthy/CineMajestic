using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs.Bills;
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
        private ObservableCollection<SeatForShowTimeDTO> dSGhe;
        public ObservableCollection<SeatForShowTimeDTO> DSGhe//load danh sách ghế 
        {
            get => dSGhe;
            set
            {
                dSGhe = value;
                OnPropertyChanged(nameof(DSGhe));
            }
        }

        public ObservableCollection<SeatForShowTimeDTO> DSGheChon;//danh sách ghế chọn

        public ICommand SelectedSeatCommand {  get; set; }

        private void Seat()
        {
            loadSeat();
            SelectedSeatCommand = new ViewModelCommand(selectedSeat);
        }

        List<string> checkSeats;//phục vụ việc check ghế có người mua chưa
        private void loadSeat()
        {
            if (showTimeDTO != null)
            {
                SeatForShowTimeDA seatForShowTimeDA = new SeatForShowTimeDA();
                DSGhe = seatForShowTimeDA.getDSGhe(showTimeDTO.Id);
                DSGheChon = new ObservableCollection<SeatForShowTimeDTO>();
                orderDTO.DSGheChon = DSGheChon;
                checkSeats = new List<string>();
                foreach(var item in DSGhe)
                {
                    if (item.Condition)
                    {
                        checkSeats.Add(item.Location);
                    }
                }
            }
        }


        private void selectedSeat(object obj)
        {
            if (obj is SeatForShowTimeDTO seatForShowTimeDTO)
            {
                if (!checkSeats.Contains(seatForShowTimeDTO.Location))
                {
                    if (Seats == "Hiện chưa chọn ghế nào!")
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
                        DSGheChon.Add(seatForShowTimeDTO);
                    }
                    else
                    {
                        TotalPriceTicket -= showTimeDTO.PerSeatTicketPrice;
                        DSGheChon.Remove(seatForShowTimeDTO);

                        if (Seats.Contains(','))
                        {
                            string[] kq = Seats.Split(',');
                            Seats = "";
                            foreach (string item in kq)
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
                else
                {
                    MessageBox.Show("Ghế đã có người mua rồi!");
                }
            }
        }
    }
}
