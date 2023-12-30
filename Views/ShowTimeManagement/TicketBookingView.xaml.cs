using CineMajestic.Models.DTOs.Bills;
using CineMajestic.Models.DTOs.ShowTimeManagement;
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
    /// Interaction logic for TicketBookingView.xaml
    /// </summary>
    public partial class TicketBookingView : Window
    {
        public TicketBookingView(ShowTimeDTO showTimeDTO,OrderDTO orderDTO)
        {
            InitializeComponent();
            TicketBookingViewModel ticketBookingViewModel= new TicketBookingViewModel(showTimeDTO,orderDTO);
            this.DataContext = ticketBookingViewModel;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
