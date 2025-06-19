using LaBarracaBar.Models;
using LaBarracaBar.Repositories;
using LaBarracaBar.Views.Controls;
using LaBarracaBar.Views.Dialogs;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using static LaBarracaBar.Views.Controls.ToastNotification;

namespace LaBarracaBar.ViewModels
{
    public class TableViewModel : ViewModelBase
    {
        public int TableId { get; set; }
        public string TableNumber { get; set; }
        public ObservableCollection<ProductItem> Products { get; set; } = new();
        public event Action<TableViewModel> OnDeleteRequested;
        public Action<string, string> ToastAction { get; set; }
        public Action<string, ToastNotification.ToastType> ToastActions { get; set; }

        public ICommand EditCommand { get; set; }
        public ICommand ChargeCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public TableViewModel(string tableNumber, List<ProductItem> products, int tableId)
        {
            TableId = tableId;
            TableNumber = $"Mesa #{tableNumber}";
            foreach (var p in products)
                Products.Add(p);
            InitCommands();
        }

        public TableViewModel(string tableNumber, List<ProductItem> products)
        {
            var repo = new TemporaryTableRepository();
            TableId = repo.AddTable(tableNumber, products); // ✅ Solo acá
            TableNumber = $"Mesa #{tableNumber}";
            foreach (var p in products)
                Products.Add(p);
            InitCommands();
        }

        private void InitCommands()
        {
            EditCommand = new RelayCommand(OnEdit);
            ChargeCommand = new RelayCommand(OnCharge);
            DeleteCommand = new RelayCommand(OnDelete);
        }
        private async void OnEdit()
        {
            var dialog = new EditTableDialog(TableNumber.Replace("Mesa #", ""), Products.ToList());
            if (dialog.ShowDialog() == true)
            {
                // Actualizar la colección en memoria
                Products.Clear();
                foreach (var p in dialog.ResultProducts)
                    Products.Add(p);

                // Actualizar en la base de datos
                var repo = new TemporaryTableRepository();
                repo.UpdateTable(TableId, dialog.ResultProducts);
                ToastAction?.Invoke("Mesa editada con éxito", "success");
                await Task.Delay(500);
            }
        }
        private async void OnDelete()
        {
            var repo = new TemporaryTableRepository();
            repo.DeleteTable(TableNumber.Replace("Mesa #", ""));

            //ToastNotification.ShowStatic($"{TableNumber} eliminada", ToastType.Info);
            ToastActions?.Invoke($"{TableNumber} eliminada", ToastNotification.ToastType.Info);

            await Task.Delay(1500);

            OnDeleteRequested?.Invoke(this); // ✅ Notifica al contenedor que debe eliminar esta instancia
        }

        private void OnCharge()
        {
            // Convertir los ProductItem (sin ID ni precio) en ProductModel con toda la info
            var repo = new ProductRepository();
            var fullProducts = new List<ProductModel>();

            foreach (var item in Products)
            {
                var product = repo.GetByName(item.Name);
                if (product != null)
                {
                    product.Quantity = item.Quantity; // Seteamos la cantidad consumida
                    fullProducts.Add(product);
                }
            }

            // Abrir el diálogo de facturación
            var chargeDialog = new ChargeTableDialog(TableNumber.Replace("Mesa #", ""), fullProducts);
            if (chargeDialog.ShowDialog() == true)
            {
                ToastNotification.ShowStatic("Mesa facturada correctamente", ToastNotification.ToastType.Success);
                OnDeleteRequested?.Invoke(this); // ✅ Notifica al contenedor que debe eliminar esta instancia
            }
            
        }
    }
}