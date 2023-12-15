using CineMajestic.ViewModels.VoucherManagement;
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

namespace CineMajestic.Views.VoucherManagement
{
    /// <summary>
    /// Interaction logic for VoucherManagementView.xaml
    /// </summary>
    public partial class VoucherManagementView : UserControl
    {
        public VoucherManagementView()
        {
            InitializeComponent();
            VoucherManagementViewModel viewModel = new VoucherManagementViewModel();
            this.DataContext= viewModel;
        }
    }
}
