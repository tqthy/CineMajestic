using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DataAccessLayer.Bills;
using CineMajestic.Models.DTOs.Bills;
using CineMajestic.Models.DTOs.ShowTimeManagement;
using CineMajestic.Views.MessageBox;
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
            BillView billView = new BillView(orderDTO,Staff_Id,  ticketBookingView,  foodBookingView);
            billView.ShowDialog();
        }
    }


    public class BillViewModel : MainBaseViewModel
    {
        public ICommand BackCommand { get; set; }
        public ICommand Paycommand { get; set; }
        public List<string> DSVC {  get; set; }



        private BillView billView;
        private OrderDTO orderDTO;
        public BillDTO billDTO { get; set; }
        public ShowTimeDTO showTimeDTO { get; set; }//dùng để bind lên hóa đơn
        private int Staff_Id;
        TicketBookingView ticketBookingView;
        FoodBookingView foodBookingView;
        public BillViewModel(BillView billView, OrderDTO orderDTO,int Staff_Id, TicketBookingView ticketBookingView, FoodBookingView foodBookingView)
        {
            this.billView = billView;
            this.orderDTO = orderDTO;
            showTimeDTO = orderDTO.showTimeDTO;
            BackCommand = new ViewModelCommand(Back);
            Paycommand = new ViewModelCommand(Pay,canPay);
            this.Staff_Id = Staff_Id;
            loadBillDTO();
            this.ticketBookingView = ticketBookingView;
            this.foodBookingView= foodBookingView;
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

            VoucherDA voucherDA = new VoucherDA();
            DSVC = voucherDA.listCodeOk();

         
        }

        private string selectedVC = "";
        public string SelectedVC
        {
            get => selectedVC;
            set
            {
                selectedVC = value;
                OnPropertyChanged(nameof(SelectedVC));
            }
        }
        private void Back(object obj)
        {
            billView.Close();
        }

        private void Pay(object obj)
        {
            //add custom hoặc lấy id custom trước để có id custom
            int point = billDTO.Total / 1000;
            CustomerDA customerDA = new CustomerDA();
            if (!customerDA.checkCustom(billDTO.PhoneNumber))//nếu khách hàng chưa tồn tại
            {
                customerDA.addCustom(new Models.DTOs.CustomerDTO(billDTO.PhoneNumber, billDTO.FullName, billDTO.Email, point));
            }
            else
            {
                customerDA.updateCustomBySDT(point, billDTO.PhoneNumber);
            }

            //add bill
            setData();
            billDTO.Customer_Id = customerDA.identCurrent();
            BillDA billDA = new BillDA();
            billDA.addBill(billDTO);


            //chuyển trạng thái ghế của showtime
            SeatForShowTimeDA seatForShowTimeDA = new SeatForShowTimeDA();
            foreach (var item in orderDTO.DSGheChon)
            {
                seatForShowTimeDA.choiceSeat(item);
            }


            int bill_Id = billDA.identCurrent();
            BillDetailDA billDetailDA = new BillDetailDA();
            ProductDA productDA = new ProductDA();
            //add chi tiết hóa đơn
            foreach (var item in orderDTO.DSSPChon)
            {
                billDetailDA.addBillDetail(bill_Id, item);
                productDA.updateQuantity(item.Id, item.Quantity_Choice);
            }

            YesMessageBox mb = new YesMessageBox("Thông báo", "Hoàn tất");
            mb.ShowDialog();
            billView.Close();
            foodBookingView.Close();
            ticketBookingView.Close();
        }

        private bool canPay(object obj)
        {
            return billDTO._canAccept[0] && billDTO._canAccept[1] && billDTO._canAccept[2];
        }


        private void setData()
        {
            billDTO.Staff_Id = Staff_Id;
            billDTO.Showtime_Id = showTimeDTO.Id;
            billDTO.BillDate = DateTime.Now.ToString("yyyy-MM-dd");
            billDTO.PerTicketPrice = showTimeDTO.PerSeatTicketPrice;
            try
            {
                billDTO.QuantityTicket = billDTO.TotalPriceTicket / showTimeDTO.PerSeatTicketPrice;
            }
            catch { }

            try
            {
                billDTO.Discount = int.Parse(changeDiscount);
            }
            catch { billDTO.Discount = 0; }
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
