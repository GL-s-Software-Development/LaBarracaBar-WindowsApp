using LaBarracaBar.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace LaBarracaBar
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            ShowLogin();
        }

        private void ShowLogin()
        {
            var loginView = new V_Login();
            loginView.Show();

            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    var mainView = new V_Main();
                    mainView.Show();
                    loginView.Close();
                    mainView.Closed += (s2, ev2) =>
                    {
                        ShowLogin();
                    };
                }
            };
        }
    }
}
