using Notifications.Wpf;
using System.Windows.Controls;
using System.Windows.Media;

namespace LaBarracaBar.Views.Controls
{
    public partial class ModernNotification : UserControl
    {
        public ModernNotification(string title, string message, NotificationType type)
        {
            InitializeComponent();
            TitleText.Text = title;
            MessageText.Text = message;

            // Colores/icónos básicos por tipo (ajustá a gusto)
            switch (type)
            {
                case NotificationType.Success:
                    NotifIcon.Icon = FontAwesome.Sharp.IconChar.CircleCheck;
                    MainBorder.Background = new SolidColorBrush(Color.FromRgb(27, 94, 32));  // verde oscuro
                    break;
                case NotificationType.Warning:
                    NotifIcon.Icon = FontAwesome.Sharp.IconChar.TriangleExclamation;
                    MainBorder.Background = new SolidColorBrush(Color.FromRgb(217, 155, 35)); // entre amarillo y ambar
                    break;
                case NotificationType.Error:
                    NotifIcon.Icon = FontAwesome.Sharp.IconChar.CircleXmark;
                    MainBorder.Background = new SolidColorBrush(Color.FromRgb(136, 14, 24)); // rojo oscuro
                    break;
                default:
                    NotifIcon.Icon = FontAwesome.Sharp.IconChar.CircleInfo;
                    MainBorder.Background = (Brush)new BrushConverter().ConvertFromString("#1D2857");
                    break;
            }
        }
    }
}