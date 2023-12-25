using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.MovieManagementVM
{
    public partial class MovieManagementViewModel
    {
        public ICommand DeleteMovieCommand {  get; set; }

        private void Delete()
        {
            DeleteMovieCommand = new ViewModelCommand(deleteMovie);
        }
        private void deleteMovie(object obj)
        {
            MovieDA movieDA= new MovieDA();
            movieDA.deleteMovie(obj as MovieDTO);
            MessageBox.Show("Xóa thành công");
            loadData();
        }
    }
}
