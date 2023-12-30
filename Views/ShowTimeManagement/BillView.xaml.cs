using CineMajestic.Models.DTOs.Bills;
using CineMajestic.ViewModels.ShowTimeManagementVM;
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

namespace CineMajestic.Views.ShowTimeManagement
{
    /// <summary>
    /// Interaction logic for BillView.xaml
    /// </summary>
    public partial class BillView : Window
    {
        public BillView(OrderDTO orderDTO,int Staff_Id)
        {
            InitializeComponent();
            BillViewModel billViewModel = new BillViewModel(this,orderDTO, Staff_Id);
            this.DataContext = billViewModel;
        }
    }
}
