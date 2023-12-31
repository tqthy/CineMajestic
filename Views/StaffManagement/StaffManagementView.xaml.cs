﻿using CineMajestic.ViewModels.StaffManagementVM;
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
    /// Interaction logic for StaffManagementView.xaml
    /// </summary>
    public partial class StaffManagementView : UserControl
    {
        public StaffManagementView(int StaffID)
        {
            InitializeComponent();
            StaffManageVM staffManageVM = new StaffManageVM(this,StaffID);
            this.DataContext = staffManageVM;
        }
    }
}
