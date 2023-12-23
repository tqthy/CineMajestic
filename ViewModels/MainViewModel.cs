using CineMajestic.Models.DTOs;
using CineMajestic.Views;
using CineMajestic.Views.MovieManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Input;

namespace CineMajestic.ViewModels
{
    public class MainViewModel : MainBaseViewModel
    {
        public MainViewModel(int Staff_Id) : base(Staff_Id)
        {
            
        }
    }
}
