using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs.ShowTimeManagement;
using CineMajestic.Views.MessageBox;
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
                YesNoMessageBox yesNoMessageBox = new YesNoMessageBox("Thông báo", "Bạn có muốn xóa suất chiếu này?");
                yesNoMessageBox.ShowDialog();
                if (yesNoMessageBox.DialogResult == false)
                {
                    return;
                }
                else
                {
                    ShowTimeDTO showTimeDTO = (ShowTimeDTO)obj;
                    ShowTimeDA showTimeDA = new ShowTimeDA();
                    if (showTimeDA.checkShowtime(showTimeDTO))
                    {
                        YesMessageBox yesMessage = new YesMessageBox("Thông báo", "Suất chiếu đang được chiếu!");
                        yesMessage.ShowDialog();
                    }
                    else
                    {
                        showTimeDA.deleteShowtime(showTimeDTO);
                        YesMessageBox yesMessage = new YesMessageBox("Thông báo", "Xóa xuất chiếu thành công");
                        yesMessage.ShowDialog();
                        loadData(phong);
                    }
                }
                
            }
        }
    }
}
