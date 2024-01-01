using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using CineMajestic.Views.MovieManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.ViewModels.MovieManagementVM
{
    public partial class MovieManagementViewModel:MainBaseViewModel
    {
        private ObservableCollection<MovieDTO> DSPhim_All;
        private ObservableCollection<MovieDTO> DSPhim_DPH;//đang phát hành
        private ObservableCollection<MovieDTO> DSPhim_NPH;//ngưng phát hành

        private MovieManagementView movieManagementView; //Dùng để giãn column khi edit,xóa,hoặc thêm

        public MovieManagementViewModel()
        {
            AddMovie();
            // SearchMovie(); gọi ở loaddata r
            Delete();
            MovieDetail();
            Filter();
            edit();
        }

        //Hàm load column
       /* private void loadWithColumn()
        {
            if (movieManagementView != null) 
            {
                movieManagementView.clName.Width = 0;
                movieManagementView.clName.Width = double.NaN;

                movieManagementView.clStatus.Width = 0;
                movieManagementView.clStatus.Width = double.NaN;

                movieManagementView.clTime.Width = 0;
                movieManagementView.clTime.Width = double.NaN;

                movieManagementView.clCountry.Width = 0;
                movieManagementView.clCountry.Width = double.NaN;

                movieManagementView.clDirector.Width = 0;
                movieManagementView.clDirector.Width = double.NaN;
            }

        }*/

    }
}
