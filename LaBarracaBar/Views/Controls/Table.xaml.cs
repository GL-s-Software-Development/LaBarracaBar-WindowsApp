using LaBarracaBar.Repositories;
using LaBarracaBar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static LaBarracaBar.Views.Controls.ToastNotification;
using UserControl = System.Windows.Controls.UserControl;

namespace LaBarracaBar.Views.Controls
{
    /// <summary>
    /// Lógica de interacción para Table.xaml
    /// </summary>
    public partial class Table : UserControl
    {
        public Action<string, string> ToastAction { get; set; }
        public Table()
        {
            InitializeComponent();
            ToastNotification.RegisterInstance(Toast);
        }
        public void ShowLocalToast(string message, ToastNotification.ToastType type)
        {
            Toast.Show(message, type);
        }
    }
}
