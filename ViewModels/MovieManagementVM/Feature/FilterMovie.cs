using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.MovieManagementVM
{
    public partial class MovieManagementViewModel
    {
        private void Filter()
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
            if (SelectedItemIndex == 1)
            {
                FilterDSPhim = DSPhim_DPH;
            }
            else if (SelectedItemIndex == 2)
            {
                FilterDSPhim = DSPhim_NPH;
            }
            else
            {
                FilterDSPhim = DSPhim_All;
            }
        }


        private void loadData()
        {
            DSPhim_All = new ObservableCollection<MovieDTO>();
            DSPhim_DPH = new ObservableCollection<MovieDTO>();
            DSPhim_NPH = new ObservableCollection<MovieDTO>();

            MovieDA data = new MovieDA();
            ObservableCollection<MovieDTO> getDSSP = data.getAllMovie();
            foreach (var item in getDSSP)
            {
                if (item.Status == "Đang phát hành")
                {
                    DSPhim_DPH.Add(item);
                }
                else if (item.Status == "Ngưng phát hành")
                {
                    DSPhim_NPH.Add(item);
                }
                DSPhim_All.Add(item);
            }

            SearchMovie();
        }
    }
}
