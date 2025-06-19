using LaBarracaBar.Models;
using LaBarracaBar.Repositories;
using OxyPlot;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace LaBarracaBar.ViewModels
{
    public class SalesAnalysisViewModel : ViewModelBase
    {
        public DateTime StartDate { get; set; } = DateTime.Today.AddDays(-7);
        public DateTime EndDate { get; set; } = DateTime.Today;
        public ObservableCollection<TopProductModel> TopProducts { get; set; } = new();
        public PlotModel PlotModel { get; set; }

        public int TotalSales { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageDaily => TotalRevenue / (decimal)Math.Max(1, (EndDate - StartDate).TotalDays + 1);

        public string TotalSalesLabel => $"Ventas totales: {TotalSales}";
        public string TotalAmountLabel => $"Total facturado: ${TotalRevenue:F2}";
        public string AverageLabel => $"Promedio por día: ${AverageDaily:F2}";

        public ICommand FilterCommand { get; }

        public SalesAnalysisViewModel()
        {
            FilterCommand = new RelayCommand(LoadData);
            LoadData();
        }

        private void LoadData()
        {

            TopProducts.Clear();
            var repo = new SalesRepository();
            var data = repo.GetSalesAnalysis(StartDate, EndDate);

            foreach (var item in data)
                TopProducts.Add(item);

            TotalSales = data.Sum(d => d.Quantity);
            TotalRevenue = data.Sum(d => d.Revenue);

            OnPropertyChanged(nameof(TopProducts));
            OnPropertyChanged(nameof(TotalSalesLabel));
            OnPropertyChanged(nameof(TotalAmountLabel));
            OnPropertyChanged(nameof(AverageLabel));
        }
    }

    public class TopProductModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Revenue { get; set; }
    }
}
