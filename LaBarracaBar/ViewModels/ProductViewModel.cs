using LaBarracaBar.Models;
using LaBarracaBar.Repositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;

namespace LaBarracaBar.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private ProductModel selectedProduct = new ProductModel();
        private readonly ProductRepository repository = new ProductRepository();
        private readonly CategoryRepository categoryRepository = new CategoryRepository();

        public ObservableCollection<ProductModel> Products { get; set; } = new ObservableCollection<ProductModel>();
        public List<CategoryModel> Categories { get; set; } = new();

        private int? _filterCategoryId;
        public int? FilterCategoryId
        {
            get => _filterCategoryId;
            set
            {
                _filterCategoryId = value;
                OnPropertyChanged();
                LoadProducts();
            }
        }
        public Action<string, string> ToastAction { get; set; }

        public ProductModel SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                OnPropertyChanged();
            }
        }

        public int SelectedCategoryId
        {
            get => SelectedProduct.Category;
            set
            {
                SelectedProduct.Category = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand LoadCommand { get; }
        public ICommand ClearFilterCommand { get; }

        public ProductViewModel()
        {
            AddCommand = new RelayCommand(AddProduct);
            EditCommand = new RelayCommand(EditProduct);
            DeleteCommand = new RelayCommand(DeleteProduct);
            LoadCommand = new RelayCommand(LoadProducts);
            ClearFilterCommand = new RelayCommand(ClearFilter);

            LoadCategories();
            LoadProducts();
        }


        private void AddProduct()
        {
            try
            {
                repository.Add(SelectedProduct);
                LoadProducts();
                SelectedProduct = new ProductModel();
                OnPropertyChanged(nameof(SelectedProduct));
                ToastAction?.Invoke("Producto agregado con éxito.", "success");
            
            }
            catch
            {
                ToastAction?.Invoke("Error al agregar el producto.", "error");
            }
        }

        private void EditProduct()
        {
            try
            {
                repository.Edit(SelectedProduct);
                LoadProducts();
                SelectedProduct = new ProductModel();
                OnPropertyChanged(nameof(SelectedProduct));
                ToastAction?.Invoke("Producto editado con éxito.", "success");
              
            }
            catch
            {
                ToastAction?.Invoke("Error al editar el producto.", "error");
            }
        }

        private void DeleteProduct()
        {
            try
            {
                repository.Remove(SelectedProduct);
                LoadProducts();
                SelectedProduct = new ProductModel();
                OnPropertyChanged(nameof(SelectedProduct));
                ToastAction?.Invoke("Producto eliminado con éxito.", "success");
            }
            catch
            {
                ToastAction?.Invoke("Error al eliminar el producto.", "error");
            }
        }

        private void LoadProducts()
        {
            Products.Clear();

            var allProducts = repository.GetByProduct("");
            var filtered = FilterCategoryId.HasValue
                ? allProducts.Where(p => p.Category == FilterCategoryId.Value)
                : allProducts;

            foreach (var product in filtered)
                Products.Add(product);
        }
        private void ClearFilter()
        {
            FilterCategoryId = null;
        }

        private void LoadCategories()
        {
            Categories = categoryRepository.GetAll();
            OnPropertyChanged(nameof(Categories));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}