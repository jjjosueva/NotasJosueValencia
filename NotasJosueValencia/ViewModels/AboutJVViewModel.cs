using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace NotasJosueValencia.ViewModels;

internal class AboutJVViewModel
{
    public string Title => AppInfo.Name;
    public string Version => AppInfo.VersionString;
    public string MoreInfoUrl => "https://aka.ms/maui";
    public string Message => "Esta aplicación está escrita en XAML y C# con .NET MAUI.";
    public ICommand ShowMoreInfoCommand { get; }

    public AboutJVViewModel()
    {
        ShowMoreInfoCommand = new AsyncRelayCommand(ShowMoreInfo);
    }

    async Task ShowMoreInfo() =>
        await Launcher.Default.OpenAsync(MoreInfoUrl);
}
