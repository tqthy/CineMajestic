using CineMajestic.Models.DTOs;
using CineMajestic.ViewModels.InformationManagement;
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

namespace CineMajestic.Views.InformationManagement
{
    /// <summary>
    /// Interaction logic for InformationView.xaml
    /// </summary>
    public partial class InformationView : UserControl
    {
        public InformationView(int Staff_Id)
        {
            InitializeComponent();
            InformationViewModel informationViewModel = new InformationViewModel(Staff_Id);
            this.DataContext = informationViewModel;
        }
    }
}
