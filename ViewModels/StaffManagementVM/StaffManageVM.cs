using CineMajestic.Models.DTOs.StaffManagement;
using CineMajestic.Views.StaffManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CineMajestic.Models.DataAccessLayer;

namespace CineMajestic.ViewModels.StaffManagementVM
{
    public partial class StaffManageVM:BaseViewModel 
    {
       
        private ObservableCollection<StaffDTO> dsnv;
        public ObservableCollection<StaffDTO> DSNV
        {
            get => dsnv;
            set
            {
                if (dsnv != value)
                {
                    dsnv = value;
                    OnPropertyChanged(nameof(DSNV));
                }
            }
        }

        
        private StaffManagementView staffManagementView;//phục vụ việc set lại chiều rộng cột khi thêm,edit,xóa
        private int StaffId;
        public StaffManageVM(StaffManagementView staffManagementView,int StaffId)
        {
            this.StaffId = StaffId;
            DSNV = new ObservableCollection<StaffDTO>();
            loadData();
            //SearchStaff();//gọi ở hàm loaddata rồi nên không cần nữa
            addStaff();
            delete();
            editStaff();
            exportExcel();
            staffDetail();
            phatluong();
            this.staffManagementView = staffManagementView;
        }

        private void loadData()
        {
            StaffDA staffDA = new StaffDA();
            DSNV=staffDA.getDSNV();
            SearchStaff();//gọi ở đây để phòng trường hợp add 1 nhân viên
            loadWidthColumn();
        }
        
        private void loadWidthColumn()
        {
            if (staffManagementView != null)
            {
                staffManagementView.clId.Width = 0;
                staffManagementView.clId.Width = double.NaN;

                staffManagementView.clFullName.Width = 0;
                staffManagementView.clFullName.Width = double.NaN;

                //staffManagementView.clBirth.Width = 0;
                //staffManagementView.clBirth.Width = double.NaN;

                staffManagementView.clGender.Width = 0;
                staffManagementView.clGender.Width = double.NaN;

                staffManagementView.clEmail.Width = 0;
                staffManagementView.clEmail.Width = double.NaN;

                staffManagementView.clPhoneNumber.Width = 0;
                staffManagementView.clPhoneNumber.Width = double.NaN;

                //staffManagementView.clSalary.Width = 0;
                //staffManagementView.clSalary.Width = double.NaN;

                staffManagementView.clRole.Width = 0;
                staffManagementView.clRole.Width = double.NaN;
            }
        }
    }
}

