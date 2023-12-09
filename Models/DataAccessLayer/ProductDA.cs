using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineMajestic.Models.DTOs.ProductManagement;

namespace CineMajestic.Models.DataAccessLayer
{

    //Test để xem hiển thị UI đúng k
    public class ProductDA
    {
        public static ObservableCollection<ProductDTO> getDSSP()
        {
            ObservableCollection<ProductDTO> DSSP = new ObservableCollection<ProductDTO>()
            {
                new ProductDTO(1,"Coca",25,10000,2,"pack://application:,,,/Images/ProductManagement/1.jpg"),
                new ProductDTO(2,"Pepsi",10,10000,2,"pack://application:,,,/Images/ProductManagement/2.jpg"),
                new ProductDTO(3,"Gà khô theo gói",40,250000,1, "pack://application:,,,/Images/ProductManagement/3.jpg"),
                new ProductDTO(4,"Bò khô theo gói",50,400000,1, "pack://application:,,,/Images/ProductManagement/4.jpg"),
                new ProductDTO(5,"Sting",15,10000,2, "pack://application:,,,/Images/ProductManagement/5.jpg"),
                new ProductDTO(6,"Hướng dương",33,29000,1, "pack://application:,,,/Images/ProductManagement/6.jpg"),

                 new ProductDTO(7,"Coca",25,10000,2,"pack://application:,,,/Images/ProductManagement/1.jpg"),
                new ProductDTO(8,"Pepsi",10,10000,2,"pack://application:,,,/Images/ProductManagement/2.jpg"),
                new ProductDTO(9,"Gà khô theo gói",40,250000,1, "pack://application:,,,/Images/ProductManagement/3.jpg"),
                new ProductDTO(10,"Bò khô theo gói",50,400000,1, "pack://application:,,,/Images/ProductManagement/4.jpg"),
                new ProductDTO(11,"Sting",15,10000,2, "pack://application:,,,/Images/ProductManagement/5.jpg"),
                new ProductDTO(12,"Hướng dương",33,29000,1, "pack://application:,,,/Images/ProductManagement/6.jpg"),
            };
            

            return DSSP;
        }
    }
}
