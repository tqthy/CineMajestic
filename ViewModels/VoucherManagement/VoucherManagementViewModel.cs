using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using CineMajestic.Views.VoucherManagement;
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

        private VoucherManagementView voucherManagementView; //Phục vụ set lại chiều rộng khi edit,xóa thêm
        public VoucherManagementViewModel(VoucherManagementView view)
        {
            loadData();
            AddVoucher();
            editVoucher();
            exportExcel();
            VoucherDetail();
            SendVoucher();
            this.voucherManagementView = view;
        }

        void loadData()
        {
            VoucherDA voucherDA = new VoucherDA();
            DSVC = voucherDA.getDSVC();
            searchVoucher();
            loadWidthColumn();
        }
        private void loadWidthColumn()
        {
            if (voucherManagementView != null)
            {
                voucherManagementView.clCode.Width = 0;
                voucherManagementView.clCode.Width = double.NaN;

                voucherManagementView.clName.Width = 0;
                voucherManagementView.clName.Width = double.NaN;

                voucherManagementView.clType.Width = 0;
                voucherManagementView.clType.Width = double.NaN;

                voucherManagementView.clStartDate.Width = 0;
                voucherManagementView.clStartDate.Width = double.NaN;

                voucherManagementView.clFinDate.Width = 0;
                voucherManagementView.clFinDate.Width = double.NaN;

            }
        }
    }
}
