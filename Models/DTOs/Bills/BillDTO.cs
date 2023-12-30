using CineMajestic.Models.DTOs.ProductManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DTOs.Bills
{
    public class BillDTO
    {
        public int Id { get; set; }//id của hóa đơn
        public string BillDate { get; set; }//ngày hóa đơn
        public int Total { get; set; }//tổng trị giá hóa đơn

        public BillDTO() { }


        public int Staff_Id {  get; set; }//mã nhân viên tạo hóa đơn
        public int Customer_Id {  get; set; }//mã khách hàng
        public int Showtime_Id {  get; set; }//mã suất chiếu
        public int Discount {  get; set; }//tiền giảm giá
        public string Note {  get; set; }//ghi chú

        //thể hiện bên vé
        public string Interval {  get; set; }//khoảng thời gian chiếu từ lúc nào đến lúc nào
        public string NameAuditorium { get; set; }//phòng chiếu
        public string Seats {  get; set; }//danh sách chỗ ngồi
        public int QuantityTicket {  get; set; }//số lượng vé
        public string TicketPrice {  get; set; }//giá 1 vé
        public string TotalPriceTicket {  get; set; }//tổng tiền vé

        //thể hiện bên product
        public ObservableCollection<ProductDTO> DSSP {  get; set; }
        public string TotalPriceProduct { get; set; }//tổng tiền product

    }
}
