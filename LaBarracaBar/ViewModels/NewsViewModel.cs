using System.Net.Http;
using System.Threading.Tasks;
using LaBarracaBar.ViewModels;

namespace LaBarracaBar.ViewModels
{
    public class NewsViewModel : ViewModelBase
    {
        private string _changelogText;

        public string ChangelogText
        {
            get => _changelogText;
            set
            {
                _changelogText = value;
                OnPropertyChanged();
            }
        }

        public NewsViewModel()
        {
            LoadChangelogAsync();
        }

        private async void LoadChangelogAsync()
        {
            try
            {
                using var httpClient = new HttpClient();
                var url = "https://raw.githubusercontent.com/GL-s-Software-Development/LaBarracaBar-CHANGELOG/refs/heads/main/CHANGELOG.md"; // reemplazá
                var content = await httpClient.GetStringAsync(url);
                ChangelogText = content;
            }
            catch
            {
                ChangelogText = "No se pudo cargar el changelog desde GitHub.";
            }
        }
    }
}