using CineMajestic.Views.ShowTimeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CineMajestic.ViewModels.ShowTimeManagementVM
{
    public partial class ShowTimeManagementViewModel
    {
        public ICommand showEditView {  get; set; }

        void edit()
        {
            showEditView = new ViewModelCommand(showEdit);
        }
        private void showEdit(object obj)
        {
            EditShowTimeView view = new EditShowTimeView();
            view.ShowDialog();
            loadData();
        }
    }
}
