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


namespace CineMajestic.ViewModels.StatisticsVM
{
    public class StatisticsViewModel : MainBaseViewModel
    {

        #region properties

        private string incomeText;
        public string IncomeText { get => incomeText; set { incomeText = value; OnPropertyChanged(nameof(IncomeText)); } }

        private string outcomeText;
        public string OutcomeText { get => outcomeText; set { outcomeText = value; OnPropertyChanged(nameof(OutcomeText)); } }

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

        public Axis[] IOXAxes { get; set; } =
        {
            new Axis
            {
                
                Labels = new string[] {""}
            }
        };

        #endregion

        #region Commands


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
            BillDA billDA= new BillDA();
        }

        private void LoadByMonth(string month)
        {
            BillAddMovieDA billAddMovieDA = new BillAddMovieDA();
            BillAddProductDA billAddProductDA = new BillAddProductDA();
            BillDA billDA = new BillDA();

            long sum_income = billDA.GetIncomeByMonth(month);
            long sum_outcome = billAddProductDA.GetOutcomeByMonth(month);

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

            //IOSeries[0].Values = new List<string> { IncomeText};
            //IOSeries[1].Values = new List<string> { OutcomeText };

        }

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
