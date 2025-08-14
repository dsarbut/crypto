using BackendClient;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CryptoMaui.ViewModels;
public partial class LoginViewModel : ObservableObject
{
    private readonly CryptoBackClient cryptoBack;
    public LoginViewModel(CryptoBackClient cryptoBack)
    {
        this.cryptoBack = cryptoBack;
        BackendUrl = cryptoBack.BaseUrl;
    }

    [ObservableProperty]
    public partial string? Username { get; set; } = "user";

    [ObservableProperty]
    public partial string? Password { get; set; } = "password";

    [ObservableProperty]
    public partial string BackendUrl { get; set; }

    [ObservableProperty]
    public partial bool IsLoggingIn { get; set; } = false;


    [RelayCommand]
    private async Task LogIn()
    {
        IsLoggingIn = true;
        try
        {
            cryptoBack.UpdateUrl(BackendUrl);

            await cryptoBack.LoginAsync(new LoginDto()
            {
                Username = Username ?? "",
                Password = Password ?? ""
            });

            await Task.Delay(2000);

            await Shell.Current.GoToAsync("//Portfolio");
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
        finally
        {
            IsLoggingIn = false;
        }
    }
}


