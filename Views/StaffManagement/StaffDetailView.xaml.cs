using CineMajestic.Models.DTOs.StaffManagement;
using CineMajestic.ViewModels.StaffManagementVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CineMajestic.Views.StaffManagement
{
    /// <summary>
    /// Interaction logic for StaffDetailView.xaml
    /// </summary>
    public partial class StaffDetailView : Window
    {
        public StaffDetailView(StaffDTO staff)
        {
            InitializeComponent();
            StaffDetailViewModel staffDetailViewModel = new StaffDetailViewModel(staff,this);
            this.DataContext = staffDetailViewModel;
        }
    }
}
