﻿using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using CineMajestic.ViewModels.ShowTimeManagementVM;
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

namespace CineMajestic.Views.ShowTimeManagement
{
    /// <summary>
    /// Interaction logic for ShowTimeManagementView.xaml
    /// </summary>
    public partial class ShowTimeManagementView : UserControl
    {
        public ShowTimeManagementView(int Staff_Id)
        {
            InitializeComponent();
            ShowTimeManagementViewModel showTimeManagementViewModel = new ShowTimeManagementViewModel(Staff_Id);
            this.DataContext = showTimeManagementViewModel;

        }
    }
}