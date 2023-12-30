using CineMajestic.Models.DTOs.ProductManagement;
using CineMajestic.Models.DTOs.ShowTimeManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DTOs.Bills
{
    public class OrderDTO
    {
        //danh sách vật phẩm chọn
        public ObservableCollection<ProductDTO> DSSPChon {  get; set; }
        //tổng tiền order vật phẩm
        public int TotalProduct;

        //danh sách ghế chọn
        public ObservableCollection<SeatForShowTimeDTO> DSGheChon {  get; set; }
        //danh sách vị trí ghế chọn
        public string Seats { get; set; }//để xíu làm bill nhanh hơn đỡ phải gộp lại ạ
        //tổng tiền vé(số ghế chọn)
        public int TotalTicket;

        //khách hàng order

        //showtime order
        public ShowTimeDTO showTimeDTO {  get; set; }


        public OrderDTO()
        {

        }
    }
}
