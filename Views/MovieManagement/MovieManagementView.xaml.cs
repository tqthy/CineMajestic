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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CineMajestic.Views.MovieManagement
{
    /// <summary>
    /// Interaction logic for MovieManagementView.xaml
    /// </summary>
    public partial class MovieManagementView : UserControl
    {
        private bool lvLoaded;
        public MovieManagementView()
        {
            MovieManagementViewModel viewModel = new();
            InitializeComponent();
            DataContext = viewModel;
            lvLoaded = false;
        }
        private bool UserSearchFilter(object item)
        {
            if (String.IsNullOrEmpty(SearchFilterBox.Text)) return true;
            else
            {
                return ((item as MovieDTO).Title.IndexOf(SearchFilterBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private bool MovieFilter(object item)
        {
            bool search = UserSearchFilter(item);
            MovieDTO movie = item as MovieDTO;
            string filter = (FilterComboBox.SelectedItem as ComboBoxItem).Content.ToString();
            if (filter == "Trống") return search;
            if (filter == "Đang chiếu")
            {
                return IsShowing(movie) && search;
            } else 
            if (filter == "Sắp chiếu")
            {
                return IsComing(movie) && search;
            } else
            if (filter == "Dừng chiếu")
            {
                return IsStopped(movie) && search;
            }
            return search;
        }

        private bool IsShowing(MovieDTO item)
        {
            DateTime startTime = DateTime.Parse(item.StartDate);
            DateTime endTime = DateTime.Parse(item.EndDate);
            return DateTime.Now >= startTime && DateTime.Now <= endTime;
        }
        private bool IsComing(MovieDTO item)
        {
            DateTime startTime = DateTime.Parse(item.StartDate);
            return DateTime.Now < startTime;
        }
        private bool IsStopped(MovieDTO item)
        {
            DateTime endTime = DateTime.Parse(item.EndDate);
            return DateTime.Now > endTime;
        }
        private void SearchFilterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvMovies.ItemsSource);
            view.Filter = MovieFilter;
            CollectionViewSource.GetDefaultView(lvMovies.ItemsSource).Refresh();
        }
        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!lvLoaded)
            {
                return;
            }

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvMovies.ItemsSource);            
            view.Filter = MovieFilter;
            CollectionViewSource.GetDefaultView(lvMovies.ItemsSource).Refresh();
        }

        private void lvMovies_Loaded(object sender, RoutedEventArgs e)
        {
            lvLoaded = true;
        }
    }
}
