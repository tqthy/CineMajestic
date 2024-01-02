using CineMajestic.Models.DTOs.ProductManagement;
using OfficeOpenXml.Table.PivotTable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DTOs.Bills
{
    public class BillDTO:INotifyPropertyChanged
    {
        public int Id { get; set; }//id của hóa đơn
        public string BillDate { get; set; }//ngày hóa đơn

        private int total;
        public int Total//tổng trị giá hóa đơn
        {
            get => total;
            set
            {
                total = value;
                OnPropertyChanged(nameof(Total));
            }
        }

        public BillDTO() { }


        public int Staff_Id {  get; set; }//mã nhân viên tạo hóa đơn
        public int Customer_Id {  get; set; }//mã khách hàng
        public int Showtime_Id {  get; set; }//mã suất chiếu


        private int discount;
        public int Discount//tiền giảm giá
        {
            get => discount;
            set
            {
                discount = value;
                OnPropertyChanged(nameof(Discount));
            }
        }
        public string Note {  get; set; }//ghi chú

        //thể hiện bên vé
        public string Interval {  get; set; }//khoảng thời gian chiếu từ lúc nào đến lúc nào
        public string NameAuditorium { get; set; }//phòng chiếu
        public string Seats {  get; set; }//danh sách chỗ ngồi
        public int QuantityTicket {  get; set; }//số lượng vé
        public int PerTicketPrice {  get; set; }
        public string TicketPrice {  get; set; }//giá 1 vé(nhưng kiểu e sẽ cho kiểu giá 1 vé x số lượng vé)
        public string MovieTitle {  get; set; }//movie title
        public int TotalPriceTicket {  get; set; }//tổng tiền vé
        public string StartDate {  get; set; }//ngày xem phim
        public string StartAndEndTime {  get; set; }//giờ chiếu và giờ hết hạn

        //thể hiện bên product
        public ObservableCollection<ProductDTO> DSSP {  get; set; }
        public int TotalPriceProduct { get; set; }//tổng tiền product


        //khách hàng,//tạm thời sẽ xử lý lỗi người dùng ở đây,sau này phát triển sẽ sang viewmodel xử lý
        //Số điện thoại
        public bool[] _canAccept = new bool[3];
        private string phoneNumber;
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                ValidatePhoneNumber();
            }
        }

        private string phoneNumberError;
        public string PhoneNumberError
        {
            get => phoneNumberError;
            set
            {
                phoneNumberError = value;
                OnPropertyChanged(nameof(PhoneNumberError));
            }
        }


        //họ và tên
        private string fullName;
        public string FullName
        {
            get => fullName;
            set
            {
                fullName = value;
                ValidateFullName();
            }
        }

        private string fullNameError;
        public string FullNameError
        {
            get => fullNameError;
            set
            {
                fullNameError = value;
                OnPropertyChanged(nameof(FullNameError));
            }
        }



        //email
        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                ValidateEmail();
            }
        }
        private string emailError;
        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged(nameof(EmailError));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




        private void ValidateFullName()
        {
            if (string.IsNullOrWhiteSpace(FullName))
            {
                FullNameError = "Họ và tên không được để trống!";
                _canAccept[1] = false;
            }
            else
            {
                FullNameError = "";
                _canAccept[1] = true;
            }
        }

        private void ValidateEmail()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                EmailError = "Email không được để trống!";
                _canAccept[2] = false;

            }
            else if (!Email.Contains("@"))
            {
                EmailError = "Email không hợp lệ!";
                _canAccept[2] = false;
            }
            else
            {
                EmailError = "";
                _canAccept[2] = true;
            }
        }

        private void ValidatePhoneNumber()
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                PhoneNumberError = "SĐT không được để trống!";
                _canAccept[0] = false;
            }
            else if (!PhoneNumber.All(char.IsDigit))
            {
                PhoneNumberError = "Số điện thoại chỉ được chứa chữ số!";
                _canAccept[0] = false;

            }
            else
            {
                PhoneNumberError = "";
                _canAccept[0] = true;
            }
        }



    }
}
