using BackendClient;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CryptoMaui.ViewModels;
public partial class LoginViewModel(CryptoBackClient cryptoBack) : ObservableObject
{
    [ObservableProperty]
    public partial string? Username { get; set; } = "user";

    [ObservableProperty]
    public partial string? Password { get; set; } = "password";


    [RelayCommand]
    private async Task LogIn()
    {
        try
        {
            await cryptoBack.LoginAsync(new LoginDto()
            {
                Username = Username ?? "",
                Password = Password ?? ""
            });

            await Shell.Current.GoToAsync("//PortfolioPage");
        }
        catch (ApiException ex)
        {
            string title = "Internal error.";
            string message = "An internal error has occurred. Restart app.";
            if (ex.StatusCode == 401)
            {
                title = "Authentication failed.";
                message = "Expected credentials:\n\r 'Username: user, Password: password'";
            }

            await Shell.Current.DisplayAlert(title, message, "Cancel");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Connection failed.", ex.Message, "Cancel");
        }
    }
}


