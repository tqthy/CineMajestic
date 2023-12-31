﻿using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using CineMajestic.Views.MessageBox;
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
            if (obj != null)
            {
                //kiểm tra phim đang chiếu thì không được xóa
                ShowTimeDA showTimeDA = new ShowTimeDA();
                if (showTimeDA.checkShowtimeByMovie((obj as MovieDTO).Id))
                {
                    YesMessageBox mb2 = new YesMessageBox("Lỗi","Phim đang có trong ít nhất 1 suất chiếu,bạn không được phép xóa!");
                    mb2.ShowDialog();
                    return;
                }

                MovieDA movieDA = new MovieDA();
                //trước khi xóa thì phải đưa thằng khóa ngoại tham chiếu tới nó là null
                BillAddMovieDA billAddMovieDA = new BillAddMovieDA();
                billAddMovieDA.updateMovie_IdNull((obj as MovieDTO).Id);

                YesNoMessageBox mb = new YesNoMessageBox("Thông báo", "Bạn có muốn xóa phim này?");
                mb.ShowDialog();
                if (mb.DialogResult == false)
                    return;
                else
                {
                    //tiến hành xóa movie
                    movieDA.deleteMovie(obj as MovieDTO);
                    mb.Close();
                    YesMessageBox msb = new YesMessageBox("Thông báo", "Xóa thành công");
                    msb.ShowDialog();
                    msb.Close();
                    loadData();
                }
            }
           
        }


       
    }
}
