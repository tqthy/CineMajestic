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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CineMajestic.Views.StaffManagement
{
    /// <summary>
    /// Interaction logic for StaffEditView.xaml
    /// </summary>
    public partial class StaffEditView : Window
    {
        public StaffEditView(StaffDTO staff)
        {
            InitializeComponent();
            EditStaffViewModel editStaffViewModel = new EditStaffViewModel(this);
            editStaffViewModel.staffDTO = staff;
            editStaffViewModel.khoitao();
            this.DataContext = editStaffViewModel;
        }
    }
}
