using LaBarracaBar.ViewModels;
using LaBarracaBar.Views;
using LaBarracaBar.Views.Controls;
using Notifications.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace LaBarracaBar.Services
{
    public static class NotificationService
    {
        private static readonly NotificationManager _notificationManager = new NotificationManager();

        public static void Show(string title, string message, Notifications.Wpf.NotificationType type = Notifications.Wpf.NotificationType.Information)
        {
            //_notificationManager.Show(new NotificationContent
            //{
            //    Title = title,
            //    Message = message,
            //    Type = type
            //});
            var notif = new ModernNotification(title, message, type);
            _notificationManager.Show(content: notif, expirationTime: TimeSpan.FromSeconds(3));

        }


        // 🔹 Notificación de confirmación
        public static void ShowConfirmation(string message, Action<bool> callback)
        {
            var confirm = new ConfirmNotification(message, callback);
            _notificationManager.Show(content: confirm, expirationTime: TimeSpan.FromSeconds(3));
        }
    }
}


//{
//    public static class NotificationService
//{
//    private static readonly NotificationManager _manager = new NotificationManager();

//    // 🔹 Notificación estándar
//    public static void Show(string title, string message, NotificationType type = NotificationType.Information)
//    {
//        var notif = new ModernNotification(title, message, type);
//        _manager.Show(content: notif, expirationTime: TimeSpan.FromSeconds(3));
//    }

//    // 🔹 Notificación de confirmación
//    public static void ShowConfirmation(string message, Action<bool> callback)
//    {
//        var confirm = new ConfirmNotification(message, callback);
//        _manager.Show(content: confirm, expirationTime: TimeSpan.FromSeconds(0));
//    }
//}
