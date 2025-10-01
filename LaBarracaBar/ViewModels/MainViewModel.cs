using FontAwesome.Sharp;
using LaBarracaBar.Models;
using LaBarracaBar.Repositories;
using LaBarracaBar.Services;
using LaBarracaBar.Views;
using LaBarracaBar.Views.Controls;
using Notifications.Wpf;
using Notifications.Wpf.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
        public Action<string, string> ToastAction { get; set; }

        private readonly NotificationManager _notificationManager = new NotificationManager();
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
        private bool _isUserMenuVisible;
        public bool IsUserMenuVisible
        {
            get => _isUserMenuVisible;
            set
            {
                _isUserMenuVisible = value;
                OnPropertyChanged(nameof(IsUserMenuVisible));
                OnPropertyChanged(nameof(UserMenuIcon)); // actualizar el ícono
            }
        }
        public string UserMenuIcon => IsUserMenuVisible ? "AngleUp" : "AngleDown";

        //--> Commands
        public ICommand ShowV_HomeCommand { get; }
        public ICommand ShowV_UpdateCommand { get; }
        public ICommand ShowV_ManageProductCommand { get; }
        public ICommand ShowV_ManageTableCommand { get; }
        public ICommand ShowV_SalesAnalysisCommand { get; }

        public ICommand ToggleUserMenuCommand { get; }
        public ICommand EditProfileCommand { get; }
        public ICommand LogoutCommand { get; }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();
            //Initialize commands
            ShowV_HomeCommand = new ViewModelCommand(ExecuteShowV_HomeCommand);
            ShowV_ManageProductCommand = new ViewModelCommand(ExecuteShowV_ManageProductCommand);
            ShowV_ManageTableCommand = new ViewModelCommand(ExecuteShowV_ManageTableCommand);
            ShowV_SalesAnalysisCommand = new ViewModelCommand(ExecuteShowV_SalesAnalysisCommand);
            ShowV_UpdateCommand = new ViewModelCommand(ExecuteShowV_UpdatesCommand);
            ToggleUserMenuCommand = new RelayCommand(ExecuteToggleUserMenu);
            EditProfileCommand = new RelayCommand(OnEditProfile);
            LogoutCommand = new RelayCommand(OnLogout);
            //Default view
            ExecuteShowV_HomeCommand(null);
            LoadCurrentUserData();
        }
        private void ExecuteToggleUserMenu()
        {
            IsUserMenuVisible = !IsUserMenuVisible;
        }
        private void OnEditProfile()
        {
            // Aún no implementado
            NotificationService.Show("Acción no disponible", "Editar perfil aún no implementado.", Notifications.Wpf.NotificationType.Warning);
        }

        private void OnLogout()
        {
            NotificationService.ShowConfirmation("¿Seguro que deseas cerrar sesión?", result =>
            {
                if (result)
                {
                    NotificationService.Show("Sesión cerrada", "Cerraste sesión correctamente", Notifications.Wpf.NotificationType.Success);
                    Application.Current.Windows
                        .OfType<V_Main>()
                        .FirstOrDefault()?.Close();
                }
                else
                {
                    NotificationService.Show("Acción cancelada", "No se cerró la sesión", Notifications.Wpf.NotificationType.Information);
                }
            });
        }
        private void ExecuteShowV_ManageProductCommand(object obj)
        {
            CurrentChildView = new ManageProductViewModel();
            Caption = "Gestionar Productos";
            Icon = IconChar.Book;
        }  
        private void ExecuteShowV_UpdatesCommand(object obj)
        {
            CurrentChildView = new NewsViewModel();
            Caption = "Actualizaciones";
            Icon = IconChar.Wrench;
        }
        private void ExecuteShowV_HomeCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Inicio";
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
            }
        }
    }
}
