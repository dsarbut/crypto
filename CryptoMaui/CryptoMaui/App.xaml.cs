namespace CryptoMaui;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new AppShell());

#if WINDOWS
        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
        {
            window.Width = 393;
            window.Height = 815;
        }
#endif
        return window;
    }
}