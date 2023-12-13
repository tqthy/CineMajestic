using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs.StaffManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.ViewModels.StaffManagementVM
{
    public partial class StaffManageVM
    {
        private ObservableCollection<StaffDTO> filterDSNV;
        public ObservableCollection<StaffDTO> FilterDSNV
        {
            get => filterDSNV;
            set
            {
                if(filterDSNV != value)
                {
                    filterDSNV = value;
                    OnPropertyChanged(nameof(FilterDSNV));
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
                    FilterStaffList();
                }
            }
        }

        void SearchStaff()
        {
            FilterDSNV = new ObservableCollection<StaffDTO>(DSNV);//ban đầu thì không cần lọc
        }
        private void FilterStaffList()
        {
            // Cập nhật FilteredStaffList dựa trên SearchText
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilterDSNV = new ObservableCollection<StaffDTO>(DSNV);
            }
            else
            {
                FilterDSNV = new ObservableCollection<StaffDTO>(
                    DSNV.Where(s => s.FullName.ToLower().Contains(SearchText.ToLower())));
            }
        }
    }
}
