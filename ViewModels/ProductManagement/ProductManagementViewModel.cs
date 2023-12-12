using CineMajestic.Models.DTOs.ProductManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineMajestic.Models.DataAccessLayer;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using System.Reflection.Metadata;

namespace CineMajestic.ViewModels.ProductManagement
{
    
    public partial class ProductManagementViewModel:BaseViewModel
    {

        //private ObservableCollection<ProductDTO> DSSP_All = new ObservableCollection<ProductDTO>();//tất cả
        //private ObservableCollection<ProductDTO> DSSP_ThucAn = new ObservableCollection<ProductDTO>();
        //private ObservableCollection<ProductDTO> DSSP_DoUong = new ObservableCollection<ProductDTO>();
        private ObservableCollection<ProductDTO> DSSP_All;
        private ObservableCollection<ProductDTO> DSSP_ThucAn;
        private ObservableCollection<ProductDTO> DSSP_DoUong;

        private ObservableCollection<ProductDTO> dssp;
        public ObservableCollection<ProductDTO> DSSP
        {
            get { return dssp; }
            set
            {
                if (dssp != value)
                {
                    dssp = value;
                    OnPropertyChanged(nameof(DSSP));
                }
            }
        }

        public ProductManagementViewModel()
        {
            PhanLoai();
            addProduct();
            delete();
            editProduct();
        }

    }
}
