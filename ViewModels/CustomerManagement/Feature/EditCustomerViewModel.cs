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
    public class EditCustomerViewModel
    {
        private EditCustomerView wd;
        public ICommand quitCommand { get; set; }

        public EditCustomerViewModel(EditCustomerView wd)
        {
            this.wd = wd;
            quitCommand = new ViewModelCommand(quit);
        }

        private void quit(object obj)
        {
            wd.Close();
        }
    }
}
