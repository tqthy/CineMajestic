﻿using CineMajestic.Models.DTOs.Bills;
using CineMajestic.Models.DTOs.ShowTimeManagement;
using CineMajestic.Views.ShowTimeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace CineMajestic.ViewModels.ShowTimeManagementVM
{
    public partial class ShowTimeManagementViewModel
    {
        public ICommand TicketBookingCommand { get; set; }

        private void TicketBooking()
        {
            TicketBookingCommand = new ViewModelCommand(ticketBooking);
        }


        private OrderDTO orderDTO;
        private void ticketBooking(object obj)
        {
            if (obj != null)
            {
                orderDTO = new OrderDTO();
                TicketBookingView ticketBookingView = new TicketBookingView(obj as ShowTimeDTO, orderDTO,Staff_Id);
                ticketBookingView.ShowDialog();
            }
        }
    }



    public partial class TicketBookingViewModel : MainBaseViewModel
    {
        public BitmapImage ImageSource { get; set; }

        //tên phim
        public string Title { get; set; }

        //thời gian bắt đầu và kết thúc
        public string Showtime { get; set; }

        //tên phòng chiếu
        public string NameAuditorium { get; set; }

        //giá vé 1 ghế
        public int PerSeatTicketPrice { get; set; }

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

        private int totalPriceTicket;
        public int TotalPriceTicket
        {
            get => totalPriceTicket;
            set
            {
                totalPriceTicket = value;
                OnPropertyChanged(nameof(TotalPriceTicket));
            }
        }

        private ShowTimeDTO showTimeDTO;

        private OrderDTO orderDTO;
        private int Staff_Id;

        TicketBookingView ticketBookingView;
        public TicketBookingViewModel(ShowTimeDTO showTimeDTO, OrderDTO orderDTO,int Staff_Id, TicketBookingView ticketBookingView)
        {
            this.showTimeDTO = showTimeDTO;
            this.orderDTO = orderDTO;
            orderDTO.showTimeDTO = showTimeDTO;
            loadShowTimeCurrent();
            Seat();
            FoodBooking();
            this.Staff_Id = Staff_Id;
            this.ticketBookingView = ticketBookingView;
        }


        private void loadShowTimeCurrent()
        {
            if (showTimeDTO != null)
            {
                ImageSource = showTimeDTO.ImageSource;
                Title = showTimeDTO.MovieTitle;
                Showtime = showTimeDTO.ShowTime;
                NameAuditorium = showTimeDTO.NameAuditorium;
                PerSeatTicketPrice = showTimeDTO.PerSeatTicketPrice;
                Seats = "Hiện chưa chọn ghế nào!";
                TotalPriceTicket = 0;
            }
        }
    }
}
