using CineMajestic.Models.DTOs.ShowTimeManagement;
using CineMajestic.Views.ShowTimeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.ShowTimeManagementVM
{
    public partial class ShowTimeManagementViewModel
    {
        public ICommand TicketBookingCommand { get; set; }

        private void TicketBooking()
        {
            TicketBookingCommand = new ViewModelCommand(ticketBooking);
        }

        private void ticketBooking(object obj)
        {
            TicketBookingView ticketBookingView=new TicketBookingView(obj as ShowTimeDTO);
            ticketBookingView.ShowDialog();
        }
    }



    public partial class TicketBookingViewModel:MainBaseViewModel
    {
        public string ImageSource {  get; set; }

        //tên phim
        public string Title { get; set; }
        
        //thời gian bắt đầu và kết thúc
        public string Showtime { get; set; }
        
        //tên phòng chiếu
        public string NameAuditorium {  get; set; }
        
        //giá vé 1 ghế
        public int PerSeatTicketPrice {  get; set; }
        
        //danh sách ghế chọn
        private string seats;
        public string Seats
        {
            get => seats;
            set
            {
                seats = value;
                OnPropertyChanged(nameof(Seats));
            }
        }

        private int totalPrice;
        public int TotalPrice
        {
            get => totalPrice;
            set
            {
                totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private ShowTimeDTO showTimeDTO;

        public TicketBookingViewModel(ShowTimeDTO showTimeDTO)
        {
            this.showTimeDTO = showTimeDTO;
            loadShowTimeCurrent();
            loadSeat();
        }


        private void loadShowTimeCurrent()
        {
            ImageSource = MotSoPTBoTro.pathProject() + @"Images\MovieManagement\" + showTimeDTO.ImageSource;
            Title = showTimeDTO.MovieTitle;
            Showtime = showTimeDTO.ShowTime;
            NameAuditorium = showTimeDTO.NameAuditorium;
            PerSeatTicketPrice= showTimeDTO.PerSeatTicketPrice;
            Seats = "Hiện chưa chọn ghế nào!";
            TotalPrice = 0;
        }
    }
}
