using CineMajestic.Models.DTOs.ProductManagement;
using OfficeOpenXml.Table.PivotTable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
        public string TicketPrice {  get; set; }//giá 1 vé(nhưng kiểu e sẽ cho kiểu giá 1 vé x số lượng vé)
        public string MovieTitle {  get; set; }//movie title
        public int TotalPriceTicket {  get; set; }//tổng tiền vé
        public string StartDate {  get; set; }//ngày xem phim
        public string StartAndEndTime {  get; set; }//giờ chiếu và giờ hết hạn

        //thể hiện bên product
        public ObservableCollection<ProductDTO> DSSP {  get; set; }
        public int TotalPriceProduct { get; set; }//tổng tiền product





        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
