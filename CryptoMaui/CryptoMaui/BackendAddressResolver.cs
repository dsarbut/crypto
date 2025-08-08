using BackendClient;

namespace CryptoMaui;
internal class BackendAddressResolver : IBackendAddressResolver
{
    const string LocalHostDockerWindows = @"https://localhost:5001";
    const string Phone = @"https://192.168.0.141:5001";

    public BackendAddressResolver()
    {
        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
            BackendAddress = Phone;
        else
            BackendAddress = LocalHostDockerWindows;
    }


    public string BackendAddress { get; }
}
