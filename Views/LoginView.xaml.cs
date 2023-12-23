using CineMajestic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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

namespace CineMajestic.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public bool isClosed;
        public LoginView()
        {
            isClosed = false;
            InitializeComponent();

            LoginViewModel loginViewModel = new LoginViewModel(this);
            this.DataContext= loginViewModel;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            isClosed = true;
            Application.Current.Shutdown();
        }

        //private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        //{
        //    if (this.DataContext != null)
        //    {
        //        ((dynamic)this.DataContext).Password = ((PasswordBox)sender).SecurePassword;
        //    }

        //}
    }
}
