using CineMajestic.Models.DTOs;
using CineMajestic.ViewModels.ErrorManagementVM;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;

namespace CineMajestic.Views.ErrorManagement
{
    /// <summary>
    /// Interaction logic for ErrorManagementView.xaml
    /// </summary>
    public partial class ErrorManagementView : UserControl
    {

        private bool lvLoaded;

        public ErrorManagementView(int Staff_Id)
        {
            ErrorManagementViewModel vm = new(Staff_Id);
            InitializeComponent();
            DataContext = vm;
            lvLoaded = false;
        }

        private bool ErrorFilter(object item)
        {
            ErrorDTO error = item as ErrorDTO;
            string filter = (cbBoxFilter.SelectedItem as ComboBoxItem).Content.ToString();
            if (filter == "Toàn bộ") return true;
            if (filter == "Chờ tiếp nhận")
            {
                return error.Status == "Chờ tiếp nhận";
            }
            else
            if (filter == "Đang xử lí")
            {
                return error.Status == "Đang xử lí";
            }
            else
            if (filter == "Đã xử lí")
            {
                return error.Status == "Đã xử lí";
            }
            return error.Status == "Đã hủy";

        }
        private void cbBoxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!lvLoaded)
            {
                return;
            }

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvError.ItemsSource);
            view.Filter = ErrorFilter;
            CollectionViewSource.GetDefaultView(lvError.ItemsSource).Refresh();
        }

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            lvLoaded = true;
        }
    }
}
