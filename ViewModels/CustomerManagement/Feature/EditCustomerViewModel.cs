using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using CineMajestic.Views.CustomerManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

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
            EditCustomerView editCustomerView = new EditCustomerView((CustomerDTO)obj);
            editCustomerView.ShowDialog();
            loadData();
        }
    }
    public class EditCustomerViewModel
    {
        private EditCustomerView wd;
        public CustomerDTO customer {  get; set; } //current customer
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? RegDate { get; set; }
        public int Point { get; set; }
        public ICommand quitCommand { get; set; }
        public ICommand acceptCommand { get; set; }
        public EditCustomerViewModel(EditCustomerView wd)
        {
            this.wd = wd;
            quitCommand = new ViewModelCommand(quit);
            acceptCommand = new ViewModelCommand(accept);
        }

        public void loadEditCurrent()
        {
            FullName = customer.FullName;
            PhoneNumber = customer.PhoneNumber;
            Email = customer.Email;

            string[] regDate = customer.RegDate.Split('/');
            RegDate = new DateTime(int.Parse(regDate[2]), int.Parse(regDate[1]), int.Parse(regDate[0]));

            Point = customer.Point;
           
        }
        private void quit(object obj)
        {
            wd.Close();
        }
        private void accept(object obj)
        {
            CustomerDA customerDA = new CustomerDA();

            //sau này nhớ xử lý lỗi người dùng
            customerDA.editCustomer(new CustomerDTO(customer.Id,FullName,PhoneNumber,Email, RegDate.Value.ToString("yyyy-MM-dd"),Point));
            MessageBox.Show("Sửa thành công");
            wd.Close();
        }
    }
}
