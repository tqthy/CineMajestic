using CineMajestic.Views;
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

        protected void ApplicationStart(object sender, EventArgs e)
        {
            var mainView = new MainView();
            mainView.Show();
        }
    }

}
