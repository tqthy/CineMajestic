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

        public StaffManageVM()
        {
            DSNV = new ObservableCollection<StaffDTO>();
            loadData();
            //SearchStaff();//gọi ở hàm loaddata rồi nên không cần nữa
            addStaff();
            delete();
        }

        private void loadData()
        {
            StaffDA staffDA = new StaffDA();
            DSNV=staffDA.getDSNV();
            SearchStaff();//gọi ở đây để phòng trường hợp add 1 nhân viên
        }
        

    }
}

//public ICommand OpenStaffAddCommand { get; set; }
//public StaffManageVM()
//{
//  //  OpenStaffAddCommand = new ViewModelCommand(OpenStaffAdd);
//    StaffAdd();
//}

//void StaffAdd()
//{
//    OpenStaffAddCommand = new ViewModelCommand(OpenStaffAdd);
//}

//private void OpenStaffAdd(object obj)
//{
//    StaffAddView staffAdd = new StaffAddView();
//    staffAdd.ShowDialog();
//}
