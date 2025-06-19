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
            var loginVew = new V_Login();

            loginVew.IsVisibleChanged += (s, ev) =>
            {
                if (loginVew.IsVisible == false && loginVew.IsLoaded)
                {
                    var mainView = new V_Main();
                    mainView.Show();
                    loginVew.Close();
                }
            };
        }
    }
}
