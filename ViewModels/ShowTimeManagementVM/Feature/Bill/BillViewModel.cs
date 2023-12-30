using CineMajestic.Models.DTOs.Bills;
using CineMajestic.Models.DTOs.ShowTimeManagement;
using CineMajestic.Views.ShowTimeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static QRCoder.PayloadGenerator;

namespace CineMajestic.ViewModels.ShowTimeManagementVM
{
    public partial class FoodBookingViewModel
    {
        public ICommand ContinueCommand { get; set; }


        private void Bill()
        {
            ContinueCommand = new ViewModelCommand(Continue);
        }

        private void Continue(object obj)
        {
            orderDTO.TotalProduct = TotalPrice;
            foreach (var item in DSSPChon)
            {
                item.TotalPrice_QuantityChoice = item.Price * item.Quantity_Choice;
            }
            BillView billView = new BillView(orderDTO);
            billView.ShowDialog();
        }
    }


    public class BillViewModel : MainBaseViewModel
    {
        public ICommand BackCommand { get; set; }
        public ICommand Paycommand { get; set; }


        private BillView billView;
        private OrderDTO orderDTO;
        public BillDTO billDTO { get; set; }
        public ShowTimeDTO showTimeDTO { get; set; }//dùng để bind lên hóa đơn
        public BillViewModel(BillView billView, OrderDTO orderDTO)
        {
            this.billView = billView;
            this.orderDTO = orderDTO;
            showTimeDTO = orderDTO.showTimeDTO;
            BackCommand = new ViewModelCommand(Back);
            Paycommand = new ViewModelCommand(Pay);
            loadBillDTO();
        }


        private void loadBillDTO()
        {
            billDTO = new BillDTO();
            billDTO.DSSP = orderDTO.DSSPChon;

            billDTO.TotalPriceProduct = orderDTO.TotalProduct;

            billDTO.MovieTitle = showTimeDTO.MovieTitle;

            string[] s1 = showTimeDTO.StartTime.Split(' ');
            billDTO.StartDate = s1[0];

            string[] s2 = showTimeDTO.EndTime.Split(' ');
            billDTO.StartAndEndTime = s1[1] + " - " + s2[1];


            billDTO.NameAuditorium = showTimeDTO.NameAuditorium;

            billDTO.Seats = orderDTO.Seats;

            string[] s3 = billDTO.Seats.Split(',');
            billDTO.TicketPrice = showTimeDTO.PerSeatTicketPrice + "x" + s3.Length;

            billDTO.TotalPriceTicket = orderDTO.TotalTicket;

            billDTO.Total = billDTO.TotalPriceTicket + billDTO.TotalPriceProduct - billDTO.Discount;

            changeDiscount = "0";
            Total = billDTO.Total;
        }

        private void Back(object obj)
        {
            billView.Close();
        }

        private void Pay(object obj)
        {

        }


        private int Total;
        private string changeDiscount;
        public string ChangeDiscount
        {
            get { return changeDiscount; }
            set
            {
                if (changeDiscount != value)
                {
                    changeDiscount = value;
                    OnPropertyChanged(nameof(ChangeDiscount));

                    if (string.IsNullOrWhiteSpace(changeDiscount))
                    {
                        billDTO.Total = Total;
                    }
                    else
                    {
                        if (changeDiscount.All(char.IsDigit))
                        {
                            billDTO.Total = Total - int.Parse(changeDiscount);
                        }
                    }

                }
            }
        }
    }
}
