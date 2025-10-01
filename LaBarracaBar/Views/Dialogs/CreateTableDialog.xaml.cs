using LaBarracaBar.Models;
using LaBarracaBar.Repositories;
using LaBarracaBar.Services;
using LaBarracaBar.Views.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace LaBarracaBar.Views.Dialogs
{
    public partial class CreateTableDialog : Window
    {
        public string TableNumber { get; set; }
        public string Category { get; set; }
        public ObservableCollection<ProductOption> ProductOptions { get; set; } = new();
        public ObservableCollection<ProductOption> FilteredProductOptions { get; set; } = new();
        public List<string> AvailableCategories => ProductOptions.Select(p => p.Category).Distinct().OrderBy(c => c).ToList();

        private string _selectedCategoryFilter;
        public string SelectedCategoryFilter
        {
            get => _selectedCategoryFilter;
            set
            {
                _selectedCategoryFilter = value;
                FilterProducts();
            }
        }
        private void FilterProducts()
        {
            FilteredProductOptions.Clear();

            var filtered = string.IsNullOrWhiteSpace(SelectedCategoryFilter)
                ? ProductOptions.ToList()
                : ProductOptions.Where(p => p.Category.Equals(SelectedCategoryFilter, StringComparison.OrdinalIgnoreCase)).ToList();

            foreach (var item in filtered)
                FilteredProductOptions.Add(item);
        }
        private void OnClearFilter(object sender, RoutedEventArgs e)
        {
            cbbCategory.Text = null;
            FilterProducts();
        }
        public List<ProductItem> SelectedProducts =>
            ProductOptions.Where(p => p.Quantity > 0)
                          .Select(p => new ProductItem { Name = p.Name, Quantity = p.Quantity })
                          .ToList();

        public CreateTableDialog()
        {
            InitializeComponent();
            DataContext = this;

            // Cargar productos desde la DB
            var repo = new ProductRepository();
            var products = repo.GetByProduct(""); // todos

            foreach (var p in products)
            {
                ProductOptions.Add(new ProductOption
                {
                    Name = p.Name,
                    Quantity = 0,
                    Category = p.CategoryName // ← esto depende de cómo lo traés de la base
                });
            }
            FilterProducts();
        }

        private void OnCancel(object sender, RoutedEventArgs e) => DialogResult = false;

        private void OnCreate(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TableNumber) || SelectedProducts.Count == 0)
            {
                NotificationService.Show("Upss", "Debe ingresar el número de mesa y al menos un producto.", Notifications.Wpf.NotificationType.Error);
                return;
            }
            NotificationService.Show("Mesa #"+TableNumber+"", "Ha sido creada correctamente.", Notifications.Wpf.NotificationType.Success);

            DialogResult = true;
        }

        public class ProductOption
        {
            public string Name { get; set; }
            public int Quantity { get; set; }
            public string Category { get; set; } // ← necesario para el filtro
        }
    }
}