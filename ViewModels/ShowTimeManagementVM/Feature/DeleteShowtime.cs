using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs.ShowTimeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.ShowTimeManagementVM
{
    public partial class ShowTimeManagementViewModel
    {
        public ICommand DeleteShowtimeCommand {  get; set; }

        private void delete()
        {
            DeleteShowtimeCommand=new ViewModelCommand(DeleteShowtime);
        }

        private void DeleteShowtime(object obj)
        {
            if(obj != null)
            {
                ShowTimeDTO showTimeDTO = (ShowTimeDTO)obj;

                ShowTimeDA showTimeDA = new ShowTimeDA();
                if (showTimeDA.checkShowtime(showTimeDTO))
                {
                    MessageBox.Show("Suất chiếu đang được chiếu!");
                }
                else
                {
                    showTimeDA.deleteShowtime(showTimeDTO);
                    MessageBox.Show("Xóa suất chiếu thành công!");
                    loadData(phong);
                }
            }
        }
    }
}
