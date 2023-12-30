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
            if (SelectedItemIndex == 1)
            {
                FilterDSPhim = new ObservableCollection<MovieDTO>(DSPhim_DPH);
            }
            else if(SelectedItemIndex == 2)
            {
                FilterDSPhim = new ObservableCollection<MovieDTO>(DSPhim_NPH);
            }
            else
            {
                FilterDSPhim = new ObservableCollection<MovieDTO>(DSPhim_All);
            }
        }


        private void FilterMovieList()
        {
            // Cập nhật FilteredStaffList dựa trên SearchText
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                if (SelectedItemIndex == 1)
                {
                    FilterDSPhim = new ObservableCollection<MovieDTO>(DSPhim_DPH);
                }
                else if (SelectedItemIndex == 2)
                {
                    FilterDSPhim = new ObservableCollection<MovieDTO>(DSPhim_NPH);
                }
                else
                {
                    FilterDSPhim = new ObservableCollection<MovieDTO>(DSPhim_All);
                }
            }
            else
            {
                if (SelectedItemIndex == 1)
                {
                    FilterDSPhim = new ObservableCollection<MovieDTO>(
                        DSPhim_DPH.Where(s => s.Title.ToLower().Contains(SearchText.ToLower())));
                }
                else if (SelectedItemIndex == 2)
                {
                    FilterDSPhim = new ObservableCollection<MovieDTO>(
                        DSPhim_NPH.Where(s => s.Title.ToLower().Contains(SearchText.ToLower())));
                }
                else
                    FilterDSPhim = new ObservableCollection<MovieDTO>(
                        DSPhim_All.Where(s => s.Title.ToLower().Contains(SearchText.ToLower())));
            }
        }

    }
}
