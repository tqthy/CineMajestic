using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CineMajestic.ViewModels.ShowTimeManagementVM
{
    public partial class FoodBookingViewModel
    {
        public ICommand FilterAllCommand {  get; set; }
        public ICommand FilterThucAnCommand {  get; set; }
        public ICommand FilterDoUongCommand {  get; set; }

        private void Filter()
        {
            FilterAllCommand = new ViewModelCommand(FilterAll);
            FilterThucAnCommand= new ViewModelCommand(FilterThucAn);
            FilterDoUongCommand=new ViewModelCommand(FilterDoUong);

        }

        private void FilterAll(object obj)
        {
            loadDSSP(0);
        }

        private void FilterThucAn(object obj)
        {
            loadDSSP(1);
        }

        private void FilterDoUong(object obj)
        {
            loadDSSP(2);
        }
    }
}
