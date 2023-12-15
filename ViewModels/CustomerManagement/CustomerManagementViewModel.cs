using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.ViewModels.CustomerManagement
{
    public partial class CustomerManagementViewModel: MainBaseViewModel
    {
        private ObservableCollection<CustomerDTO> dsctm;
        public ObservableCollection<CustomerDTO> DSCTM
        {
            get => dsctm;
            set
            {
                if (dsctm != value)
                {
                    dsctm = value;
                    OnPropertyChanged(nameof(DSCTM));
                }
            }
        }

        public CustomerManagementViewModel()
        {
            loadData();
            editCustomer();
        }

        void loadData()
        {
            CustomerDA customerDA = new CustomerDA();
            DSCTM = customerDA.getDSCustomer();
        }
    }
}
