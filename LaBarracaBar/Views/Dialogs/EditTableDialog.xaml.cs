using LaBarracaBar.Models;
using LaBarracaBar.Repositories;
using LaBarracaBar.Services;
using LaBarracaBar.Views.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using static LaBarracaBar.Views.Controls.ToastNotification;

namespace LaBarracaBar.Views.Dialogs
{
    public partial class EditTableDialog : Window
    {
        public string TableTitle { get; set; }
        public string Category { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        public ObservableCollection<ProductOption> ProductOptions { get; set; } = new();
        public ObservableCollection<ProductOption> FilteredProductOptions { get; set; } = new();
        private readonly CategoryRepository categoryRepository = new CategoryRepository();
        public Action<string, string> ToastAction { get; set; }
        public List<string> AvailableCategories => new[] { "" }.Concat(ProductOptions.Select(p => p.Category).Distinct().OrderBy(c => c)).ToList();

        private string _selectedCategoryFilter;
        public string SelectedCategoryFilter
        {
            get => _selectedCategoryFilter;
            set
            {
                _selectedCategoryFilter = value;
                OnPropertyChanged();
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
            cbbCategory.Text = null ;
            FilterProducts(); // en realidad no es necesario si está dentro del setter
        }
        public List<ProductItem> ResultProducts =>
            ProductOptions.Where(p => p.Quantity > 0)
                          .Select(p => new ProductItem { Name = p.Name, Quantity = p.Quantity })
                          .ToList();

        public EditTableDialog(string tableNumber, List<ProductItem> existingProducts)
        {
            InitializeComponent();
            DataContext = this;

            TableTitle = $"Mesa #{tableNumber}";

            var repo = new ProductRepository();
            var allProducts = repo.GetByProduct("");

            foreach (var p in allProducts)
            {
                var existing = existingProducts.FirstOrDefault(x => x.Name == p.Name);
                ProductOptions.Add(new ProductOption
                {
                    Name = p.Name,
                    Quantity = existing?.Quantity ?? 0, // ✅ Cargar cantidad real si ya existía
                    Category = p.CategoryName
                });

            }
            FilterProducts();
        }

        private void OnCancel(object sender, RoutedEventArgs e) => DialogResult = false;

        private async void OnSave(object sender, RoutedEventArgs e)
        {
            if (ResultProducts.Count == 0)
            {
                //Toast.Show("Debe ingresar al menos un producto con cantidad mayor a 0.", ToastNotification.ToastType.Error);
                NotificationService.Show(""+TableTitle+"", "Debe ingresar al menos un producto con cantidad mayor a 0.", Notifications.Wpf.NotificationType.Information);
                return;
            }
            //Toast.Show("Cambios guardados correctamente", ToastNotification.ToastType.Success);
            NotificationService.Show(""+TableTitle+"", "Mesa editada con éxito.", Notifications.Wpf.NotificationType.Success);
            await Task.Delay(1000);

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