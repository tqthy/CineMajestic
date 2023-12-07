using CineMajestic.ViewModels;
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

namespace CineMajestic.Views.MovieManagement
{
    /// <summary>
    /// Interaction logic for MovieManagementView.xaml
    /// </summary>
    public partial class MovieManagementView : UserControl
    {
        public MovieManagementView()
        {
            MovieManagementViewModel viewModel = new();
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
