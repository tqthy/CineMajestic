using CineMajestic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.ViewModels.CustomerManagement
{
    public partial class CustomerManagementViewModel
    {
        public string cboLuaChonTimKiem {  get; set; }

        void searchCTM()
        {
            cboLuaChonTimKiem = "";
            FilterDSCTM = new ObservableCollection<CustomerDTO>(DSCTM);
        }

        private ObservableCollection<CustomerDTO> filterDSCTM;
        public ObservableCollection<CustomerDTO> FilterDSCTM
        {
            get => filterDSCTM;
            set
            {
                if (filterDSCTM != value)
                {
                    filterDSCTM = value;
                    OnPropertyChanged(nameof(FilterDSCTM));
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
                    FilterCustomerList();
                }
            }
        }

        private void FilterCustomerList()
        {
            // Cập nhật FilterVoucherList dựa trên SearchText
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilterDSCTM = new ObservableCollection<CustomerDTO>(DSCTM);
            }
            else
            {
                if (cboLuaChonTimKiem == "Tên khách hàng")
                {
                    FilterDSCTM = new ObservableCollection<CustomerDTO>(
                        DSCTM.Where(s => s.FullName.ToLower().Contains(SearchText.ToLower())));
                }
                else if(cboLuaChonTimKiem=="Số điện thoại")
                {
                    FilterDSCTM = new ObservableCollection<CustomerDTO>(
                        DSCTM.Where(s => s.PhoneNumber.ToLower().Contains(SearchText.ToLower())));
                }
            }
        }

    }
}
