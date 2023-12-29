using Microsoft.Xaml.Behaviors.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DTOs.ShowTimeManagement
{
    public class ShowTimeDTO
    {
        public int Id {  get; set; }
        public string StartTime {  get; set; }//ngày và giờ chiếu
        public string ShowTime {  get; set; }//giờ chiếu
        public string EndTime { get; set; }
        public int PerSeatTicketPrice {  get; set; }
        public int MovieId {  get; set; }
        public string MovieTitle {  get; set; }
        public int Length {  get; set; }
        public int Auditorium_Id {  get; set; }
        public string ImageSource {  get; set; }
        public string NameAuditorium {  get; set; }

        //constructor phục vụ lấy ds ShowTime
        public ShowTimeDTO(int id,string startTime,string endTime,int perSeatTicketPrice,int movieId,string movieTitle,int length,string imageSource,string nameAuditorium)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            MovieId= movieId;
            PerSeatTicketPrice = perSeatTicketPrice;
            MovieTitle = movieTitle;
            Length=length;
            ImageSource=imageSource;
            NameAuditorium=nameAuditorium;
            //giờ chiếu
            string[]s=StartTime.Split(' ');
            ShowTime = s[1];
        }


        //constructor phục vụ add 1 showtime
        public ShowTimeDTO(string startTime, string endTime, int perSeatTicketPrice, int movieId,int auditorium_Id)
        {
            StartTime = startTime;
            EndTime= endTime;
            MovieId = movieId;
            PerSeatTicketPrice = perSeatTicketPrice;
            Auditorium_Id= auditorium_Id;
        }


    }
}
