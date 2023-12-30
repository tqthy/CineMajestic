using CineMajestic.Models.DTOs;
using CineMajestic.ViewModels.MovieManagementVM;
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

namespace CineMajestic.Views.MovieManagement
{
    /// <summary>
    /// Interaction logic for MovieDetailView.xaml
    /// </summary>
    public partial class MovieDetailView : Window
    {
        public MovieDetailView(MovieDTO movieDTO)
        {
            InitializeComponent();
            MovieDetailViewModel movieDetailViewModel = new MovieDetailViewModel(movieDTO);
            this.DataContext = movieDetailViewModel;
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void wdDetail_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
