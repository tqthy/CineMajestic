using CineMajestic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.ViewModels.MovieManagementVM
{
    public partial class MovieManagementViewModel
    {
        private ObservableCollection<MovieDTO> filterDSPhim;
        public ObservableCollection<MovieDTO> FilterDSPhim
        {
            get => filterDSPhim;
            set
            {
                if (filterDSPhim != value)
                {
                    filterDSPhim = value;
                    OnPropertyChanged(nameof(FilterDSPhim));
                }
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
                    FilterMovieList();
                }
            }
        }


        void SearchMovie()
        {
            FilterDSPhim = new ObservableCollection<MovieDTO>(DSPhim);//ban đầu thì không cần lọc
        }


        private void FilterMovieList()
        {
            // Cập nhật FilteredStaffList dựa trên SearchText
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilterDSPhim = new ObservableCollection<MovieDTO>(DSPhim);
            }
            else
            {
                FilterDSPhim = new ObservableCollection<MovieDTO>(
                    DSPhim.Where(s => s.Title.ToLower().Contains(SearchText.ToLower())));
            }
        }

    }
}
