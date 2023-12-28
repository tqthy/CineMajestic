using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CineMajestic.Views.CustomerManagement;
using CineMajestic.Views.ErrorManagement;
using CineMajestic.Views.InformationManagement;
using CineMajestic.Views.MovieManagement;
using CineMajestic.Views.ProductManagement;
using CineMajestic.Views.ShowTimeManagement;
using CineMajestic.Views.StaffManagement;
using CineMajestic.Views.Statistics;
using CineMajestic.Views.VoucherManagement;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System.Windows.Controls;
using CineMajestic.Models.DataAccessLayer.Bills;
using System.Diagnostics;
using System.Globalization;
using LiveChartsCore.SkiaSharpView.Painting;
using CineMajestic.Models.DataAccessLayer;
using System.Collections.ObjectModel;
using CineMajestic.Models.DTOs;


namespace CineMajestic.ViewModels.StatisticsVM
{
    public class StatisticsViewModel : MainBaseViewModel
    {

        #region properties

        private string incomeText;
        public string IncomeText { get => incomeText; set { incomeText = value; OnPropertyChanged(nameof(IncomeText)); } }

        private string outcomeText;
        public string OutcomeText { get => outcomeText; set { outcomeText = value; OnPropertyChanged(nameof(OutcomeText)); } }

        // overall

        private ComboBoxItem _SelectedIncomePeriod;
        public ComboBoxItem SelectedIncomePeriod
        {
            get { return _SelectedIncomePeriod; }
            set { _SelectedIncomePeriod = value; OnPropertyChanged(nameof(SelectedIncomePeriod)); }
        }

        private string _SelectedIncomeTime;
        public string SelectedIncomeTime
        {
            get { return _SelectedIncomeTime; }
            set { _SelectedIncomeTime = value; OnPropertyChanged(nameof(SelectedIncomeTime)); }
        }

        // customer

        private ObservableCollection<CustomerStatisticsDTO> customerList = new();
        public ObservableCollection<CustomerStatisticsDTO> CustomerList
        {
            get => customerList;
            set { customerList = value; OnPropertyChanged(nameof(CustomerList)); }
        }

        private ComboBoxItem _SelectedCustomerIncomePeriod;
        public ComboBoxItem SelectedCustomerIncomePeriod
        {
            get { return _SelectedCustomerIncomePeriod; }
            set { _SelectedCustomerIncomePeriod = value; OnPropertyChanged(nameof(SelectedCustomerIncomePeriod)); }
        }

        private string _SelectedCustomerIncomeTime;
        public string SelectedCustomerIncomeTime
        {
            get { return _SelectedCustomerIncomeTime; }
            set { _SelectedCustomerIncomeTime = value; OnPropertyChanged(nameof(SelectedCustomerIncomeTime)); }
        }

        

        // current view

        private object currentStatisticsView;
        public object CurrentStatisticsView
        {
            get { return currentStatisticsView; }
            set { currentStatisticsView = value; OnPropertyChanged(nameof(CurrentStatisticsView)); }
        }


        #endregion

        #region livecharts

        private ISeries[] _IOSeries;
        public ISeries[] IOSeries { get { return _IOSeries; } set { _IOSeries = value; OnPropertyChanged(nameof(IOSeries)); } }

        private ISeries[] _OPSeries;
        public ISeries[] OPSeries { get { return _OPSeries; } set { _OPSeries = value; OnPropertyChanged(nameof(OPSeries)); } }

        private ISeries[] _IPSeries;
        public ISeries[] IPSeries { get { return _IPSeries; } set { _IPSeries = value; OnPropertyChanged(nameof(IPSeries)); } }

        public Axis[] IOXAxes { get; set; } =
        {
            new Axis
            {
                
                Labels = new string[] {""}
            }
        };

        #endregion

        #region Commands

        // overall
        public ICommand ChangeIncomePeriodCommand { get; set; }

        private void ExecuteChangeIncomePeriodCM(object obj)
        {
            if (SelectedIncomePeriod == null) return;
            else
            {
                switch (SelectedIncomePeriod.Content.ToString())
                {
                    case "Theo năm":
                        {
                            if (SelectedIncomeTime != null)
                            {
                                LoadByYear(SelectedIncomeTime);
                            }
                            return;
                        }
                    case "Theo tháng":
                        {
                            if (SelectedIncomeTime != null)
                            {
                                LoadByMonth(SelectedIncomeTime.Substring(6));
                            }
                            return;
                        }
                }
            }
        }

        private void LoadByYear(string year)
        {
            BillAddMovieDA billAddMovieDA = new BillAddMovieDA();
            BillAddProductDA billAddProductDA = new BillAddProductDA();
            BillImportProductDA billImportProductDA = new BillImportProductDA();
            MovieDA movieDA = new MovieDA();
            BillDA billDA = new BillDA();
            ErrorDA errorDA = new ErrorDA();

            try
            {
                long errorCostByYear = errorDA.GetCostByYear(year);
                long addProductCostByYear = billAddProductDA.GetOutcomeByYear(year) + billImportProductDA.GetOutcomeByYear(year);
                long movieCostByYear = movieDA.GetCostByYear(year);
                long sum_income = billDA.GetIncomeByYear(year);
                long product_income = billDA.GetProductIncomeByYear(year);
                long sum_outcome = addProductCostByYear + errorCostByYear + movieCostByYear;

                IncomeText = sum_income.ToString("N0");
                OutcomeText = sum_outcome.ToString("N0");

                IOSeries = new ISeries[]
                {
                    new ColumnSeries<long>
                    {
                        Values = new List<long>{ sum_income},
                        Name = "Doanh thu"
                    },
                    new ColumnSeries<long>
                    {
                        Values = new List<long>{ sum_outcome },
                        Name = "Chi phí"
                    }
                };
                OPSeries = new ISeries[]
                {
                    new PieSeries<long>
                    {
                        Values = new long[] {errorCostByYear},
                        Name = "Sự cố"
                    },
                    new PieSeries<long>
                    {
                        Values = new long[] {addProductCostByYear},
                        Name = "Nhập thực phẩm"
                    },
                    new PieSeries<long>
                    {
                        Values = new long[] {movieCostByYear},
                        Name = "Nhập phim"
                    }
                };
                IPSeries = new ISeries[]
                {
                    new PieSeries<long>
                    {
                        Values = new long[] {sum_income-product_income},
                        Name = "Vé"
                    },
                    new PieSeries<long>
                    {
                        Values = new long[] {product_income},
                        Name = "Sản phẩm"
                    }
                };

            }
            catch (Exception ex)
            {

            }


        }

        private void LoadByMonth(string month)
        {
            BillAddMovieDA billAddMovieDA = new BillAddMovieDA();
            BillAddProductDA billAddProductDA = new BillAddProductDA();
            BillImportProductDA billImportProductDA = new BillImportProductDA();
            BillDA billDA = new BillDA();
            ErrorDA errorDA = new ErrorDA();
            MovieDA movieDA = new MovieDA();

            try
            {
                long errorCostByMonth = errorDA.GetCostByMonth(month);
                long addProductCostByMonth = billAddProductDA.GetOutcomeByMonth(month) + billImportProductDA.GetOutcomeByYear(month);
                long movieCostByMonth = movieDA.GetCostByMonth(month);
                long sum_income = billDA.GetIncomeByMonth(month);
                long product_income = billDA.GetProductIncomeByMonth(month);
                long sum_outcome = addProductCostByMonth + errorCostByMonth + movieCostByMonth;

                IncomeText = sum_income.ToString("N0");
                OutcomeText = sum_outcome.ToString("N0");

                IOSeries = new ISeries[]
                {
                    new ColumnSeries<long>
                    {
                        Values = new List<long>{ sum_income},
                        Name = "Doanh thu"
                    },
                    new ColumnSeries<long>
                    {
                        Values = new List<long>{ sum_outcome },
                        Name = "Chi phí"
                    }
                };
                OPSeries = new ISeries[]
                {
                    new PieSeries<long>
                    {
                        Values = new long[] {errorCostByMonth},
                        Name = "Sự cố"
                    },
                    new PieSeries<long>
                    {
                        Values = new long[] {addProductCostByMonth},
                        Name = "Nhập thực phẩm"
                    },
                    new PieSeries<long>
                    {
                        Values = new long[] {movieCostByMonth},
                        Name = "Nhập phim"
                    }
                };
                IPSeries = new ISeries[]
                {
                    new PieSeries<long>
                    {
                        Values = new long[] {sum_income-product_income},
                        Name = "Vé"
                    },
                    new PieSeries<long>
                    {
                        Values = new long[] {product_income},
                        Name = "Sản phẩm"
                    }
                };

            } catch (Exception ex)
            {

            }



            //IOSeries[0].Values = new List<string> { IncomeText};
            //IOSeries[1].Values = new List<string> { OutcomeText };

        }

        // customer

        public ICommand ChangeCustomerIncomePeriodCommand { get; set; }

        private void ExecuteChangeCustomerIncomePeriodCM(object obj)
        {
            if (SelectedCustomerIncomePeriod == null) return;
            else
            {
                switch (SelectedCustomerIncomePeriod.Content.ToString())
                {
                    case "Theo năm":
                        {
                            if (SelectedCustomerIncomeTime != null)
                            {
                                LoadCustomerByYear(SelectedCustomerIncomeTime);
                            }
                            return;
                        }
                    case "Theo tháng":
                        {
                            if (SelectedCustomerIncomeTime != null)
                            {
                                LoadCustomerByMonth(SelectedCustomerIncomeTime.Substring(6));
                            }
                            return;
                        }
                }
            }
        }

        private void LoadCustomerByMonth(string month)
        {
            CustomerDA customerDA = new CustomerDA();
            List<CustomerStatisticsDTO> cusList = customerDA.GetTopCustomerByMonth(month);
        }
        private void LoadCustomerByYear(string year)
        {

        }

        // switch view
        public ICommand SwitchViewStatisticsCommand { get; set; }

        private void SwitchViewStatistics(object userControlName)
        {
            string UserControlName = userControlName as string;
            switch (UserControlName)
            {
                case "Movie":
                    //CurrentStatisticsView = new StatisticsMovieView();
                    break;
                case "Overall":
                    CurrentStatisticsView = new StatisticsOverallView();
                    break;
                case "Product":
                    //CurrentStatisticsView = new StatisticsProductView();
                    break;
                case "Customer":
                    CurrentStatisticsView = new StatisticsCustomerView();
                    break;
            }
        }

        #endregion

        public StatisticsViewModel()
        {
            CurrentStatisticsView = new StatisticsOverallView();
            SwitchViewStatisticsCommand = new ViewModelCommand(SwitchViewStatistics);
            ChangeIncomePeriodCommand = new ViewModelCommand(ExecuteChangeIncomePeriodCM);
        }


    }
}
