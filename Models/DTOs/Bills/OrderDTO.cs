using CineMajestic.Models.DTOs.ProductManagement;
using CineMajestic.Models.DTOs.ShowTimeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DTOs.Bills
{
    public class OrderDTO
    {
        //danh sách vật phẩm chọn
        List<ProductDTO> listProduct;
        //tổng tiền order vật phẩm
        int TotalProduct;


        //danh sách ghế chọn
        List<SeatForShowTimeDTO> listSeat;
        //tổng tiền vé(số ghế chọn)
        int TotalTicket;

        //khách hàng order
        CustomerDTO customer;


        public OrderDTO()
        {
            listProduct = new List<ProductDTO>();
            listSeat= new List<SeatForShowTimeDTO>();
            customer = new CustomerDTO();
            TotalProduct = 0;
            TotalTicket = 0;
        }
    }
}
