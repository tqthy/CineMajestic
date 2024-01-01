using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using CineMajestic.Models.DTOs.StaffManagement;
using CineMajestic.Views.MessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.CustomerManagement
{
    public partial class CustomerManagementViewModel
    {
        public ICommand deleteCustomerCommand { get; set; }
        void delete()
        {
            deleteCustomerCommand = new ViewModelCommand(deleteCus);
        }

        private void deleteCus(object obj)
        {
            CustomerDA customer = new CustomerDA();
            if (obj is CustomerDTO cus)
            {
                YesNoMessageBox mb = new YesNoMessageBox("Thông báo", "Bạn có muốn xóa khách hàng này?");
                mb.ShowDialog();
                if(mb.DialogResult == false)
                {
                    return;
                }
                else
                {
                    customer.deleteCustomer(cus);
                    mb.Close();
                    YesMessageBox msb = new YesMessageBox("Thông báo", "Xóa thành công");
                    msb.ShowDialog();
                    msb.Close();
                }
                loadData();
            }
        }


    }
}
