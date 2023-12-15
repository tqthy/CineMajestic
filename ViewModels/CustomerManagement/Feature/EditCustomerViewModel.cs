using CineMajestic.Views.CustomerManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CineMajestic.ViewModels.CustomerManagement
{
    public partial class CustomerManagementViewModel
    {
        public ICommand showwdEditCustomerCommand { get; set; }

        void editCustomer()
        {
            showwdEditCustomerCommand = new ViewModelCommand(showwdEditCustomer);
        }
        private void showwdEditCustomer(object obj)
        {
            EditCustomerView editCustomerView = new EditCustomerView();
            editCustomerView.ShowDialog();
            loadData();
        }
    }
}
