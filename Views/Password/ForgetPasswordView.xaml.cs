using CineMajestic.ViewModels.ForgotPassword;
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

namespace CineMajestic.Views.Password
{
    /// <summary>
    /// Interaction logic for ForgetPasswordView.xaml
    /// </summary>
    public partial class ForgetPasswordView : Window
    {
        public ForgetPasswordView()
        {
            InitializeComponent();
            ForgotPasswordViewModel forgotPasswordViewModel = new ForgotPasswordViewModel();
            this.DataContext= forgotPasswordViewModel;
        }
    }
}
