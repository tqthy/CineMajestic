using CineMajestic.Models.DTOs.ShowTimeManagement;
using Microsoft.Xaml.Behaviors.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                    ObservableCollection<ShowTimeDTO> list = new ObservableCollection<ShowTimeDTO>();
                    foreach(var item in DSSuatChieu)
                    {
                        string[]s=item.StartTime.Split(' ');
                        if (s[0] == dateChoice)
                        {
                            list.Add(item);
                        }
                    }
                    DSSuatChieu=list;
                }
            }
        }
    }
}
