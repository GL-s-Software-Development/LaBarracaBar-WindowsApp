using LaBarracaBar.Models;
using LaBarracaBar.Repositories;
using LaBarracaBar.Services;
using LaBarracaBar.ViewModels;
using LaBarracaBar.Views.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using static LaBarracaBar.Views.Controls.ToastNotification;

namespace LaBarracaBar.Views.Dialogs
{
    public partial class ChargeTableDialog : Window, INotifyPropertyChanged
    {
        public string TableNumber { get; set; }

        private List<ProductModel> _products;
        public List<ProductModel> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
                OnPropertyChanged(nameof(Total));
                OnPropertyChanged(nameof(CanFacturar));
            }
        }

        public decimal Total => Products?.Sum(p => (decimal)p.Price * p.Quantity) ?? 0;

        public bool CanFacturar => Products != null && Products.Any() && Total > 0;

        public ChargeTableDialog(string tableNumber, List<ProductModel> products)
        {
            InitializeComponent();
            DataContext = this;
            TableNumber = tableNumber;
            Products = products;
        }

        private async void OnFacturar(object sender, RoutedEventArgs e)
        {
            try
            {
                // 1. Guardar en base de datos
                var salesRepo = new SalesRepository();
                int saleId = salesRepo.CreateSale(TableNumber, Products);

                // 2. Imprimir
                TicketPrinter.Print(TableNumber, Products, Total);

                // 3. Eliminar mesa
                new TemporaryTableRepository().DeleteTable(TableNumber);

                // 4. Confirmar
                NotificationService.Show("Mesa #"+TableNumber+" Facturada","Ha sido facturada correctamente.", Notifications.Wpf.NotificationType.Success);
                await Task.Delay(1000);
                DialogResult = true;
            }
            catch (Exception ex)
            {
                NotificationService.Show("Error al facturar", "Error interno: ["+ex.Message+"].", Notifications.Wpf.NotificationType.Error);
            }
        }

        private async void OnCancel(object sender, RoutedEventArgs e)
        {
            NotificationService.Show("Mesa #"+TableNumber+"", "Cobro cancelado.", Notifications.Wpf.NotificationType.Information);
            await Task.Delay(1200);
            DialogResult = false;
            Close();
        }

        // INotifyPropertyChanged
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(name));
    }
}