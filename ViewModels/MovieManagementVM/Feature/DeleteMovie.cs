using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.MovieManagementVM
{
    public partial class MovieManagementViewModel
    {
        public ICommand DeleteMovieCommand { get; set; }

        private void Delete()
        {
            DeleteMovieCommand = new ViewModelCommand(deleteMovie);
        }
        private void deleteMovie(object obj)
        {
            MovieDA movieDA = new MovieDA();
            //trước khi xóa thì phải đưa thằng khóa ngoại tham chiếu tới nó là null
            BillAddMovieDA billAddMovieDA = new BillAddMovieDA();
            billAddMovieDA.updateMovie_IdNull((obj as MovieDTO).Id);

            //tiến hành xóa movie
            movieDA.deleteMovie(obj as MovieDTO);
            MessageBox.Show("Xóa thành công");
            loadData();
        }


       
    }
}
