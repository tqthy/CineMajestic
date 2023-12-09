using CineMajestic.Models.DTOs.ProductManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineMajestic.Models.DataAccessLayer;

namespace CineMajestic.ViewModels.ProductManagement
{
    //test xem hiển thị UI đúng k
    public class ProductManagementViewModel
    {
        public ObservableCollection<ProductDTO> DSSP {  get; set; }

        public ProductManagementViewModel()
        {
            DSSP=ProductDA.getDSSP();
        }

    }
}
