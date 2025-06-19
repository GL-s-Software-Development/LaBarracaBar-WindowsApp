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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LaBarracaBar.Views.Controls
{
    public partial class ToastNotification : UserControl
    {
        public ToastNotification()
        {
            InitializeComponent();
        }
        private static ToastNotification _instance;
        public static void RegisterInstance(ToastNotification instance)
        {
            _instance = instance;
        }

        public static void ShowStatic(string message, ToastType type)
        {
            _instance?.Show(message, type);
        }

        public enum ToastType
        {
            Info,
            Success,
            Error
        }

        public void Show(string message, ToastType type = ToastType.Info, int duration = 3000)
        {
            MessageText.Text = message;

            // Establecer el color de fondo
            switch (type)
            {
                case ToastType.Success:
                    MainBorder.Background = new SolidColorBrush(Color.FromRgb(40, 167, 69)); // verde
                    ToastIcon.Text = "✔";
                    break;
                case ToastType.Error:
                    MainBorder.Background = new SolidColorBrush(Color.FromRgb(220, 53, 69)); // rojo
                    ToastIcon.Text = "✖";
                    break;
                default:
                    MainBorder.Background = new SolidColorBrush(Color.FromRgb(51, 51, 51)); // gris oscuro
                    ToastIcon.Text = "⚠";
                    break;
            }

            // Hacer visible
            Visibility = Visibility.Visible;
            MainBorder.Opacity = 0;

            // Crear el storyboard
            var storyboard = new Storyboard();

            // Animación de entrada (fade in)
            var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(300));
            Storyboard.SetTarget(fadeIn, MainBorder);
            Storyboard.SetTargetProperty(fadeIn, new PropertyPath(Border.OpacityProperty));
            storyboard.Children.Add(fadeIn);

            // Animación de salida (fade out después del tiempo deseado)
            var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(300))
            {
                BeginTime = TimeSpan.FromMilliseconds(duration)
            };
            fadeOut.Completed += (s, e) => Visibility = Visibility.Collapsed;
            Storyboard.SetTarget(fadeOut, MainBorder);
            Storyboard.SetTargetProperty(fadeOut, new PropertyPath(Border.OpacityProperty));
            storyboard.Children.Add(fadeOut);

            storyboard.Begin();
        }
    }
}