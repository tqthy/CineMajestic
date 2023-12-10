using CineMajestic.Models.DTOs.ProductManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CineMajestic.Models.DataAccessLayer;


namespace CineMajestic.ViewModels.ProductManagement
{

    //tính năng dành cho phân loại ở combobox cboPhanLoai
    public partial class ProductManagementViewModel
    {
        void PhanLoai()//coi như là hàm khởi tạo cho tính năng
        {
            loadData();
            cboSelectionChangedCommand = new ViewModelCommand(CboSelectionChanged);
        }



        public ICommand cboSelectionChangedCommand { get; set; }


        private int _selectedItemIndex;
        public int SelectedItemIndex
        {
            get { return _selectedItemIndex; }
            set
            {
                if (_selectedItemIndex != value)
                {
                    _selectedItemIndex = value;
                    OnPropertyChanged(nameof(SelectedItemIndex));
                }
            }
        }

        private void CboSelectionChanged(object obj)
        {
            // Thực hiện xử lý khi giá trị được chọn thay đổi ở đây
            HandleSelectionChanged();
        }

        private void HandleSelectionChanged()
        {
            if(SelectedItemIndex == 1)
            {
                DSSP = DSSP_ThucAn;
            }
            else if(SelectedItemIndex == 2)
            {
                DSSP = DSSP_DoUong;
            }
            else
            {
                DSSP = DSSP_All;
            }
        }

        private void loadData()
        {
            ProductDA data = new ProductDA();
            ObservableCollection<ProductDTO> getDSSP = data.getDSSP();
            foreach (var item in getDSSP)
            {
                if (item.Type == 1)
                {
                    DSSP_ThucAn.Add(item);
                }
                else if(item.Type == 2)
                {
                    DSSP_DoUong.Add(item);
                }
                DSSP_All.Add(item);
            }
        }
    }
}
