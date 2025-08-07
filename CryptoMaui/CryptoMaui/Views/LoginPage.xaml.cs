using BackendClient;
using CryptoMaui.ViewModels;
using System.Diagnostics;

namespace CryptoMaui.Views;

partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        BindingContext = loginViewModel;
    }
}
