using CineMajestic.Models.DTOs.ProductManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.ViewModels.ProductManagement
{
    public partial class ProductManagementViewModel
    {
        private ObservableCollection<ProductDTO> filterDSSP;
        public ObservableCollection<ProductDTO> FilterDSSP
        {
            get => filterDSSP;
            set
            {
                if(filterDSSP != value)
                {
                    filterDSSP = value;
                    OnPropertyChanged(nameof(FilterDSSP));
                }
            }
        }

        void SearchProduct()
        {
            if (SelectedItemIndex == 1)
            {
                FilterDSSP = new ObservableCollection<ProductDTO>(DSSP_ThucAn);
            }
            else if(SelectedItemIndex==2)
            {
                FilterDSSP = new ObservableCollection<ProductDTO>(DSSP_DoUong);
            }
            else
            {
                FilterDSSP=new ObservableCollection<ProductDTO>(DSSP_All);
            }
        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                    FilterProductList();
                }
            }
        }

        private void FilterProductList()
        {
            // Cập nhật FilteredStaffList dựa trên SearchText
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                if (SelectedItemIndex == 1)
                {
                    FilterDSSP = new ObservableCollection<ProductDTO>(DSSP_ThucAn);
                }
                else if(SelectedItemIndex == 2)
                {
                    FilterDSSP = new ObservableCollection<ProductDTO>(DSSP_DoUong);
                }
                else
                {
                    FilterDSSP = new ObservableCollection<ProductDTO>(DSSP_All);
                }
            }
            else
            {
                if (SelectedItemIndex == 1)
                {
                    FilterDSSP = new ObservableCollection<ProductDTO>(
                        DSSP_ThucAn.Where(s => s.Name.ToLower().Contains(SearchText.ToLower())));
                }
                else if(SelectedItemIndex == 2)
                {
                    FilterDSSP = new ObservableCollection<ProductDTO>(
                        DSSP_DoUong.Where(s => s.Name.ToLower().Contains(SearchText.ToLower())));
                }
                else
                    FilterDSSP = new ObservableCollection<ProductDTO>(
                        DSSP_All.Where(s => s.Name.ToLower().Contains(SearchText.ToLower())));
            }
        }

    }
}
