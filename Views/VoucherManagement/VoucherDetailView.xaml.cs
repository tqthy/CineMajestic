﻿using CineMajestic.Models.DTOs;
using CineMajestic.ViewModels.VoucherManagement;
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

namespace CineMajestic.Views.VoucherManagement
{
    /// <summary>
    /// Interaction logic for VoucherDetailView.xaml
    /// </summary>
    public partial class VoucherDetailView : Window
    {
        public VoucherDetailView(VoucherDTO voucherDTO)
        {
            InitializeComponent();
            VoucherDetailViewModel model = new VoucherDetailViewModel(voucherDTO,this);
            this.DataContext = model;
        }
    }
}
