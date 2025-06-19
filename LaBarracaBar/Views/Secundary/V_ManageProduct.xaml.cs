using LaBarracaBar.ViewModels;
using LaBarracaBar.Views.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static LaBarracaBar.Views.Controls.ToastNotification;

namespace LaBarracaBar.Views.Secundary
{
    /// <summary>
    /// Lógica de interacción para V_ManageProduct.xaml
    /// </summary>
    public partial class V_ManageProduct : UserControl
    {
        public V_ManageProduct()
        {
            InitializeComponent();
            if (DataContext is ProductViewModel vm)
                vm.ToastAction = ShowToast;
        }

        public void ShowToast(string message, string type = "info")
        {
            var toastType = ToastNotification.ToastType.Info;
            if (type == "success") toastType = ToastNotification.ToastType.Success;
            else if (type == "error") toastType = ToastNotification.ToastType.Error;

            Toast.Show(message, toastType);
        }

    }
}
