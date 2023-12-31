using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs.ShowTimeManagement;
using CineMajestic.Views.MessageBox;
using CineMajestic.Views.ShowTimeManagement;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.ShowTimeManagementVM
{
    public partial class ShowTimeManagementViewModel
    {
        public ICommand AddShowTimeCommand {  get; set; }

        private void AddShowTime()
        {
            AddShowTimeCommand = new ViewModelCommand(addShowTime);
        }

        private void addShowTime(object obj)
        {
            AddShowTimeView addShowTimeView = new AddShowTimeView(phong);
            addShowTimeView.ShowDialog();

            loadData(phong);//ở phòng nào thì add suất chiếu cho phòng đó nên e cho load theo phòng đó
        }
    }

    public class AddShowTimeViewModel:MainBaseViewModel
    {
        public List<Tuple<int, string>> DSPhim {  get; set; }//danh sách phim đang phát hành

        public List<Tuple<int, string>> DSPhong { get; set; }//danh sách phòng

        //phim chọn
        public Tuple<int, string> SelectedPhim {  get; set; }

        //phòng chọn

        private Tuple<int, string> selectedPhong;
        public Tuple<int, string> SelectedPhong
        {
            get => selectedPhong;
            set
            {
                selectedPhong = value;
                OnPropertyChanged(nameof(SelectedPhong));
            }
        }

        //ngày chiếu
        private DateTime? startDate;
        public DateTime? StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
            }
        }

        //giờ chiếu
        private DateTime? showtime;
        public DateTime? Showtime
        {
            get => showtime;
            set
            {
                showtime = value;
            }
        }


        //giá vé
        private int perSeatTicketPrice;
        public int PerSeatTicketPrice
        {
            get => perSeatTicketPrice;
            set
            {
                perSeatTicketPrice = value;
            }
        }


        public ICommand AcceptCommand { get; set; }
        public ICommand CancelCommand {  get; set; }

        AddShowTimeView addShowTimeView;
        public AddShowTimeViewModel(string phong , AddShowTimeView addShowTimeView)
        {
            loadData();
            AcceptCommand = new ViewModelCommand(Accept);
            CancelCommand = new ViewModelCommand(Cancel);
            this.addShowTimeView = addShowTimeView;
            loadSelectedPhong(phong);
        }


        private void loadData()
        {
            MovieDA movieDA = new MovieDA();
            DSPhim = movieDA.getDSTitleDPH();

            AuditoriumDA auditoriumDA = new AuditoriumDA();
            DSPhong=auditoriumDA.getDSPhong();
        }


        private void Accept(object obj)
        {

            //starttime
            try
            {
                string formatStartDate = StartDate.Value.ToString("yyyy-MM-dd");
                string formatShowtime = Showtime.Value.ToString("HH:mm:ss");
                string StartTime = formatStartDate + " " + formatShowtime;
          

            //endtime
            DateTime ?endTime;

            MovieDA movieDA=new MovieDA();
            string EndTime = StartTime;
            try
            {
                endTime = DateTime.Parse(StartTime).AddMinutes(movieDA.MovieLength(SelectedPhim.Item1));
                EndTime = endTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch { }

            try
            {
                DateTime startTimeByADD = DateTime.ParseExact(StartTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                ShowTimeDA showTimeDA = new ShowTimeDA();
                if (showTimeDA.canAddShowtime(SelectedPhong.Item2, startTimeByADD))
                {

                    showTimeDA.addShowtime(new ShowTimeDTO(StartTime, EndTime, PerSeatTicketPrice, SelectedPhim.Item1, SelectedPhong.Item1));

                    YesMessageBox mb = new YesMessageBox("Thông báo", "Thêm suất chiếu thành công");
                    mb.ShowDialog();
                    addShowTimeView.Close();
                }
                else
                {
                    YesMessageBox mb = new YesMessageBox("Thông báo", "Có một suất chiếu khác đang chiếu trong khoảng thời gian này!");
                    mb.ShowDialog();
                    addShowTimeView.Close();
                }

            }
            catch { addShowTimeView.Close(); }
            }
            catch { addShowTimeView.Close(); }
        }


        private void Cancel(object obj)
        {
            addShowTimeView.Close();
        }


        private void loadSelectedPhong(string phong)
        {
            if (phong != "All")
            {
                AuditoriumDA auditoriumDA = new AuditoriumDA();
                SelectedPhong = new Tuple<int, string>(auditoriumDA.AuditoriumId(phong), phong);
            }
        }
    }
}
