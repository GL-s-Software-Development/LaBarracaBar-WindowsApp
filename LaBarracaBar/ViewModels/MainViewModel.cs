using FontAwesome.Sharp;
using LaBarracaBar.Models;
using LaBarracaBar.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LaBarracaBar.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //Fields
        private UserAccountModel _currentUserAccount;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

        private UserRepository userRepository;

        //Properties
        public UserAccountModel CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }
            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public IconChar Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }
        //--> Commands
        public ICommand ShowV_HomeCommand { get; }
        public ICommand ShowV_ManageProductCommand { get; }
        public ICommand ShowV_ManageTableCommand { get; }
        public ICommand ShowV_SalesAnalysisCommand { get; }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();
            //Initialize commands
            ShowV_HomeCommand = new ViewModelCommand(ExecuteShowV_HomeCommand);
            ShowV_ManageProductCommand = new ViewModelCommand(ExecuteShowV_ManageProductCommand);
            ShowV_ManageTableCommand = new ViewModelCommand(ExecuteShowV_ManageTableCommand);
            ShowV_SalesAnalysisCommand = new ViewModelCommand(ExecuteShowV_SalesAnalysisCommand);
            //Default view
            ExecuteShowV_HomeCommand(null);
            LoadCurrentUserData();
        }

        private void ExecuteShowV_ManageProductCommand(object obj)
        {
            CurrentChildView = new ManageProductViewModel();
            Caption = "Gestionar Productos";
            Icon = IconChar.Book;
        }        
        private void ExecuteShowV_HomeCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Novedades";
            Icon = IconChar.Home;
        }
        private void ExecuteShowV_ManageTableCommand(object obj)
        {
            CurrentChildView = new ManageTableViewModel();
            Caption = "Gestionar Mesas";
            Icon = IconChar.HardDrive;
        }
        private void ExecuteShowV_SalesAnalysisCommand(object obj)
        {
            CurrentChildView = new SalesAnalysisViewModel();
            Caption = "Análisis de Ventas";
            Icon = IconChar.ChartBar;
        }
        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserAccount.Username = user.Username;
                CurrentUserAccount.DisplayName = $"{user.Name}";
                CurrentUserAccount.ProfilePicture = null;
            }
            else
            {
                CurrentUserAccount.DisplayName = "Usuario no válido, no ha iniciado sesión";
                //Hide child views;
            }
        }
    }
}
