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
    /// Interaction logic for ErrorDoneView.xaml
    /// </summary>
    public partial class ErrorEditView : Window
    {
        public ErrorEditView()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            string stt = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();
            if (stt != "Đã xử lý")
            {
                dtpEnd.IsEnabled = false;
                txtCost.IsEnabled = false;
            }
            else
            {
                dtpEnd.IsEnabled = true;
                txtCost.IsEnabled = true;
            }
        }
    }
}
