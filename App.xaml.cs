using CineMajestic.Models.DTOs;
using CineMajestic.Views;
using CineMajestic.Views.Password;
using System.Configuration;
using System.Data;
using System.Windows;

namespace CineMajestic
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //protected void ApplicationStart(object sender, EventArgs e)
        //{
        //    var loginView = new LoginView();
        //    loginView.Show();
        //    loginView.IsVisibleChanged += (s, ev) =>
        //    {
        //        if (loginView.IsVisible == false && loginView.IsLoaded)
        //        {
        //            var mainView = new MainView();
        //            mainView.Show();
        //            loginView.Close();
        //        }
        //    };
        //}


        //test
        //private void Application_Startup(object sender, StartupEventArgs e)
        //{
        //    MainView mainView = new MainView();
        //    mainView.Show();
        //}



        private void ApplicationStart(object sender, EventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.Show();
        }


        //test
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainView mainView = new MainView();
            mainView.Show();
        }
    }

}
