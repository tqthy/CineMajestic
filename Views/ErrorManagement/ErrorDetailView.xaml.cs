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

namespace CineMajestic.Views.ErrorManagement
{
    /// <summary>
    /// Interaction logic for ErrorDetailView.xaml
    /// </summary>
    public partial class ErrorDetailView : Window
    {
        private bool loadedStatus;

        public ErrorDetailView()
        {
            InitializeComponent();
            loadedStatus = false;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void infoCost_Loaded(object sender, RoutedEventArgs e)
        {
            if (!loadedStatus)
            {
                return;
            }
            if (txtStatus.Text == "Đã xử lý")
            {
                infoCost.Visibility = Visibility.Visible;
                infoEnd.Visibility = Visibility.Visible;
            } else
            {
                infoCost.Visibility = Visibility.Hidden;
                infoEnd.Visibility =Visibility.Hidden;
            }
        }

        private void txtStatus_Loaded(object sender, RoutedEventArgs e)
        {
            loadedStatus = true;
        }
    }
}
