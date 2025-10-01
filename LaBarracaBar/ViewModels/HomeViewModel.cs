using LaBarracaBar.Repositories;
using System;
using System.ComponentModel;
using System.Windows;

namespace LaBarracaBar.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public string TodaySummary => $"Hoy es {DateTime.Today:dddd d 'de' MMMM 'de' yyyy}";

        private decimal _todayRevenue;
        public string TodayRevenue => $"${_todayRevenue:F2}";

        private int _activeTables;
        public int ActiveTables => _activeTables;

        public HomeViewModel()
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var salesRepo = new SalesRepository();
                _todayRevenue = salesRepo.GetTotalRevenueForDay(DateTime.Today);

                var tempRepo = new TemporaryTableRepository();
                _activeTables = tempRepo.GetTableCount();

                OnPropertyChanged(nameof(TodayRevenue));
                OnPropertyChanged(nameof(ActiveTables));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar resumen de inicio: " + ex.Message);
            }
        }
    }
}