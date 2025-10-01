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

namespace LaBarracaBar.Views.Controls
{
    /// <summary>
    /// Lógica de interacción para ConfirmNotification.xaml
    /// </summary>
    public partial class ConfirmNotification : UserControl
    {
        private readonly Action<bool> _callback;

        public ConfirmNotification(string message, Action<bool> callback)
        {
            InitializeComponent();
            MessageText.Text = message;
            _callback = callback;
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            _callback?.Invoke(true);
        }

        private void BtnNo_Click(object sender, RoutedEventArgs e)
        {
            _callback?.Invoke(false);
        }
    }
}
