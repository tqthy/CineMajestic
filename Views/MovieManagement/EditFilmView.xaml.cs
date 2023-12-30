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
    /// Interaction logic for EditFilmView.xaml
    /// </summary>
    public partial class EditFilmView : Window
    {
        public EditFilmView(MovieDTO movieDTO)
        {
            InitializeComponent();
            EditMovieViewModel editMovieViewModel=new EditMovieViewModel(movieDTO,this);
            this.DataContext= editMovieViewModel;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
