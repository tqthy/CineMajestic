using CineMajestic.Views.ShowTimeManagement;
using CineMajestic.Views.VoucherManagement;
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

        public class EditShowTimeViewModel
        {
            private EditShowTimeView wd;
            public ICommand CancelCommand { get; set; }

            public EditShowTimeViewModel(EditShowTimeView wd)
            {
                this.wd = wd;
                CancelCommand = new ViewModelCommand(quit);
            }

            private void quit(object obj)
            {
                wd.Close();
            }
        }
    }
}
