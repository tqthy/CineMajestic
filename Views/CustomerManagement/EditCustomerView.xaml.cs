using CineMajestic.Models.DTOs;
using CineMajestic.ViewModels.CustomerManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CineMajestic.Views.CustomerManagement
{
    /// <summary>
    /// Interaction logic for EditCustomerView.xaml
    /// </summary>
    public partial class EditCustomerView : Window
    {
        public EditCustomerView(CustomerDTO customer)
        {
            InitializeComponent();
            EditCustomerViewModel editCustomerViewModel = new EditCustomerViewModel(this);
            editCustomerViewModel.customer = customer;
            editCustomerViewModel.loadEditCurrent();
            this.DataContext = editCustomerViewModel;

        }
    }
}
