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
using SkiaSharp;
using CineMajestic.Models.DTOs.ProductManagement;
using LiveChartsCore.Defaults;


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

        // movie

        private ObservableCollection<MovieStatisticsDTO> movieList = new();
        public ObservableCollection<MovieStatisticsDTO> MovieList
        {
            get => movieList;
            set { movieList = value; OnPropertyChanged(nameof(MovieList)); }
        }

        private ComboBoxItem _SelectedMovieIncomePeriod;
        public ComboBoxItem SelectedMovieIncomePeriod
        {
            get { return _SelectedMovieIncomePeriod; }
            set { _SelectedMovieIncomePeriod = value; OnPropertyChanged(nameof(SelectedMovieIncomePeriod)); }
        }

        private string _SelectedMovieIncomeTime;
        public string SelectedMovieIncomeTime
        {
            get { return _SelectedMovieIncomeTime; }
            set { _SelectedMovieIncomeTime = value; OnPropertyChanged(nameof(SelectedMovieIncomeTime)); }
        }


        // product

        private ObservableCollection<ProductStatisticsDTO> productList = new();
        public ObservableCollection<ProductStatisticsDTO> ProductList
        {
            get => productList;
            set { productList = value; OnPropertyChanged(nameof(ProductList)); }
        }

        private ComboBoxItem _SelectedProductIncomePeriod;
        public ComboBoxItem SelectedProductIncomePeriod
        {
            get { return _SelectedProductIncomePeriod; }
            set { _SelectedProductIncomePeriod = value; OnPropertyChanged(nameof(SelectedProductIncomePeriod)); }
        }

        private string _SelectedProductIncomeTime;
        public string SelectedProductIncomeTime
        {
            get { return _SelectedProductIncomeTime; }
            set { _SelectedProductIncomeTime = value; OnPropertyChanged(nameof(SelectedProductIncomeTime)); }
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

        public Axis[] CXAxes { get; set; } = new Axis[]
        {
            new TimeSpanAxis(TimeSpan.FromHours(1), date => date.ToString("%h") + "h")
        };

        public Axis[] CYAxes { get; set; } = {
            new Axis
            {
                Name="Số lượng khách"
            }
        };
        private ISeries[] _CLSeries;
        public ISeries[] CLSeries { get { return _CLSeries; } set { _CLSeries = value; OnPropertyChanged(nameof(CLSeries)); } }

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
            CustomerList = new ObservableCollection<CustomerStatisticsDTO>(cusList);
        }
        private void LoadCustomerByYear(string year)
        {
            CustomerDA customerDA = new CustomerDA();
            List<CustomerStatisticsDTO> cusList = customerDA.GetTopCustomerByYear(year);
            CustomerList = new ObservableCollection<CustomerStatisticsDTO>(cusList);
        }

        // Movie

        public ICommand ChangeMovieIncomePeriodCommand { get; set; }

        private void ExecuteChangeMovieIncomePeriodCM(object obj)
        {
            if (SelectedMovieIncomePeriod == null) return;
            else
            {
                switch (SelectedMovieIncomePeriod.Content.ToString())
                {
                    case "Theo năm":
                        {
                            if (SelectedMovieIncomeTime != null)
                            {
                                LoadMovieByYear(SelectedMovieIncomeTime);
                            }
                            return;
                        }
                    case "Theo tháng":
                        {
                            if (SelectedMovieIncomeTime != null)
                            {
                                LoadMovieByMonth(SelectedMovieIncomeTime.Substring(6));
                            }
                            return;
                        }
                }
            }
        }

        private void LoadMovieByMonth(string month)
        {
            MovieDA movieDA = new MovieDA();
            List<MovieStatisticsDTO> movList = movieDA.GetTopMovieByMonth(month);
            MovieList = new ObservableCollection<MovieStatisticsDTO>(movList);
        }
        private void LoadMovieByYear(string year)
        {
            MovieDA movieDA = new();
            List<MovieStatisticsDTO> movList = movieDA.GetTopMovieByYear(year);
            MovieList = new ObservableCollection<MovieStatisticsDTO>(movList);
        }

        // product

        public ICommand ChangeProductIncomePeriodCommand { get; set; }

        private void ExecuteChangeProductIncomePeriodCM(object obj)
        {
            if (SelectedProductIncomePeriod == null) return;
            else
            {
                switch (SelectedProductIncomePeriod.Content.ToString())
                {
                    case "Theo năm":
                        {
                            if (SelectedProductIncomeTime != null)
                            {
                                LoadProductByYear(SelectedProductIncomeTime);
                            }
                            return;
                        }
                    case "Theo tháng":
                        {
                            if (SelectedProductIncomeTime != null)
                            {
                                LoadProductByMonth(SelectedProductIncomeTime.Substring(6));
                            }
                            return;
                        }
                }
            }
        }

        private void LoadProductByMonth(string month)
        {
            ProductDA productDA = new();
            List<ProductStatisticsDTO> pList = productDA.GetTopProductByMonth(month);
            ProductList = new ObservableCollection<ProductStatisticsDTO>(pList);
        }
        private void LoadProductByYear(string year)
        {
            ProductDA productDA = new();
            List<ProductStatisticsDTO> pList = productDA.GetTopProductByYear(year);
            ProductList = new ObservableCollection<ProductStatisticsDTO>(pList);
        }



        // switch view
        public ICommand SwitchViewStatisticsCommand { get; set; }

        private void SwitchViewStatistics(object userControlName)
        {
            string UserControlName = userControlName as string;
            switch (UserControlName)
            {
                case "Movie":
                    CurrentStatisticsView = new StatisticsMovie();
                    break;
                case "Overall":
                    CurrentStatisticsView = new StatisticsOverallView();
                    break;
                case "Product":
                    CurrentStatisticsView = new StatisticsProduct();
                    break;
                case "Customer":
                    try 
                    {
                        List<TimeSpanPoint> timeSpanPoints = new List<TimeSpanPoint>();
                        BillDA billService = new BillDA();
                        List<Tuple<int, int>> tuples = billService.GetCustomerDistribution();
                        int iT = 0; 
                        for(int h = 0; h <= 23; ++h)
                        {
                            if (iT >= tuples.Count)
                            {
                                timeSpanPoints.Add(new TimeSpanPoint(TimeSpan.FromHours(h), 0));
                            } else
                            {
                                if (tuples[iT].Item1 == h)
                                {
                                    timeSpanPoints.Add(new TimeSpanPoint(TimeSpan.FromHours(h), tuples[iT].Item2));
                                    ++iT;
                                } else
                                {
                                    timeSpanPoints.Add(new TimeSpanPoint(TimeSpan.FromHours(h), 0));
                                }
                            }
                        }

                        CLSeries = new ISeries[]
                        {
                            new LineSeries<TimeSpanPoint>
                            {
                                Values = timeSpanPoints
                            }
                        };
                    } catch
                    {

                    }
                    
                    
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
            ChangeCustomerIncomePeriodCommand = new ViewModelCommand(ExecuteChangeCustomerIncomePeriodCM);
            ChangeMovieIncomePeriodCommand = new ViewModelCommand(ExecuteChangeMovieIncomePeriodCM);
            ChangeProductIncomePeriodCommand = new ViewModelCommand(ExecuteChangeProductIncomePeriodCM);
        }


    }
}
