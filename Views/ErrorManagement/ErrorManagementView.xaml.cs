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

namespace CineMajestic.Views.ErrorManagement
{
    /// <summary>
    /// Interaction logic for ErrorManagementView.xaml
    /// </summary>
    public partial class ErrorManagementView : UserControl
    {
        public ErrorManagementView()
        {
            ErrorManagementViewModel vm = new();
            InitializeComponent();
            DataContext = vm;
        }
    }
}
