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

namespace CineMajestic.Views.Statistics
{
    /// <summary>
    /// Interaction logic for StatisticsMovie.xaml
    /// </summary>
    public partial class StatisticsMovie : UserControl
    {
        public StatisticsMovie()
        {
            InitializeComponent();
        }
        private void cbBoxChuKy_Loaded(object sender, RoutedEventArgs e)
        {
            cbBoxChuKy.SelectedIndex = 0;
            //GetMonthSource(cbBoxThoiDiem);
        }

        private void cbBoxChuKy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbBoxChuKy.SelectedItem == null) return;
            ComboBoxItem s = (ComboBoxItem)cbBoxChuKy.SelectedItem;
            switch (s.Content.ToString())
            {
                case "Theo năm":
                    {
                        GetYearSource(cbBoxThoiDiem);
                        return;
                    }
                case "Theo tháng":
                    {
                        GetMonthSource(cbBoxThoiDiem);
                        return;
                    }
            }
        }

        public void GetYearSource(ComboBox cbb)
        {
            if (cbb is null) return;

            List<string> l = new List<string>();

            int now = -1;
            for (int i = 2023; i <= System.DateTime.Now.Year; i++)
            {
                now++;
                l.Add(i.ToString());
            }
            cbb.ItemsSource = l;
            cbb.SelectedIndex = now;
        }
        public void GetMonthSource(ComboBox cbb)
        {
            if (cbb is null) return;

            List<string> l = new List<string>();

            l.Add("Tháng 1");
            l.Add("Tháng 2");
            l.Add("Tháng 3");
            l.Add("Tháng 4");
            l.Add("Tháng 5");
            l.Add("Tháng 6");
            l.Add("Tháng 7");
            l.Add("Tháng 8");
            l.Add("Tháng 9");
            l.Add("Tháng 10");
            l.Add("Tháng 11");
            l.Add("Tháng 12");

            cbb.ItemsSource = l;
            cbb.SelectedIndex = DateTime.Today.Month - 1;
        }
    }
}
