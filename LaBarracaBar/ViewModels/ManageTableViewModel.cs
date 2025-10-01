using LaBarracaBar.Models;
using LaBarracaBar.Repositories;
using LaBarracaBar.Views.Controls;
using LaBarracaBar.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LaBarracaBar.ViewModels
{
    public class ManageTableViewModel : ViewModelBase
    {
        public ObservableCollection<Table> Tables { get; set; } = new();
        public ICommand CreateTableCommand { get; }

        public ManageTableViewModel()
        {
            LoadTables();
            CreateTableCommand = new RelayCommand(CreateTable);
        }

        public void CreateTable()
        {
            var dialog = new CreateTableDialog();
            if (dialog.ShowDialog() == true)
            {
                var vm = new TableViewModel(dialog.TableNumber, dialog.SelectedProducts);
                var view = new Table
                {
                    DataContext = vm
                };

                vm.OnDeleteRequested += _ => RemoveTableView(view);
                Tables.Add(view);
            }
        }

        private void LoadTables()
        {
            Tables.Clear();
            var repo = new TemporaryTableRepository();
            var loadedTables = repo.GetAllTables();

            foreach (var (id, number, products) in loadedTables)
            {
                var vm = new TableViewModel(number, products, id);
                var view = new Table
                {
                    DataContext = vm
                };
                vm.OnDeleteRequested += _ => RemoveTableView(view);
                Tables.Add(view);
            }
        }
        private void RemoveTableView(Table view)
        {
            if (Tables.Contains(view))
            {
                Tables.Remove(view);
            }
        }
    }
}
