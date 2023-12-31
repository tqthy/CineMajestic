using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CineMajestic.ViewModels.ShowTimeManagementVM
{
    public partial class ShowTimeManagementViewModel
    {
        string dateChoice = "";
        private DateTime? filterStartDate;
        public DateTime? FilterStartDate
        {
            get => filterStartDate;
            set
            {

                filterStartDate = value;
                OnPropertyChanged(nameof(FilterStartDate));
                if (filterStartDate != null)
                {
                    dateChoice = filterStartDate.Value.ToString("dd/MM/yyyy");
                }
            }
        }
    }
}
