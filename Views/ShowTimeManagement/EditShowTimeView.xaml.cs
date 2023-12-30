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
using static CineMajestic.ViewModels.ShowTimeManagementVM.ShowTimeManagementViewModel;

namespace CineMajestic.Views.ShowTimeManagement
{
    /// <summary>
    /// Interaction logic for EditShowTimeView.xaml
    /// </summary>
    public partial class EditShowTimeView : Window
    {
        public EditShowTimeView()
        {
            InitializeComponent();
            EditShowTimeViewModel edit = new EditShowTimeViewModel(this);
            this.DataContext = edit;
        }
    }
}
