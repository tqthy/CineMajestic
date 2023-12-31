﻿using CineMajestic.ViewModels.ShowTimeManagementVM;
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

namespace CineMajestic.Views.ShowTimeManagement
{
    /// <summary>
    /// Interaction logic for AddShowTimeView.xaml
    /// </summary>
    public partial class AddShowTimeView : Window
    {
        public AddShowTimeView(string phong)
        {
            InitializeComponent();
            AddShowTimeViewModel addShowTimeViewModel = new AddShowTimeViewModel(phong,this);
            this.DataContext=addShowTimeViewModel;
        }
    }
}
