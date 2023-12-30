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
        public ObservableCollection<ProductDTO> DSSP {  get; set; }
        //tổng tiền order vật phẩm
        int TotalProduct;

        //danh sách ghế chọn
        public ObservableCollection<SeatForShowTimeDTO> DSGhe {  get; set; }
        //tổng tiền vé(số ghế chọn)
        int TotalTicket;

        //khách hàng order
        CustomerDTO customer;


        public OrderDTO()
        {
            customer = new CustomerDTO();
            TotalProduct = 0;
            TotalTicket = 0;
        }
    }
}
