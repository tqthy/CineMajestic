using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.ViewModels.VoucherManagement
{
    public partial class VoucherManagementViewModel:MainBaseViewModel
    {
        private ObservableCollection<VoucherDTO> dsvc;
        public ObservableCollection<VoucherDTO> DSVC
        {
            get => dsvc;
            set
            {
                if (dsvc != value)
                {
                    dsvc = value;
                    OnPropertyChanged(nameof(DSVC));
                }
            }
        }

        public VoucherManagementViewModel()
        {
            loadData();
        }

        void loadData()
        {
            VoucherDA voucherDA = new VoucherDA();
            DSVC = voucherDA.getDSVC();
        }
    }
}
