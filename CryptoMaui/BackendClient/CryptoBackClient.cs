using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendClient;
public partial class CryptoBackClient
{
    public CryptoBackClient(IBackendAddressResolver backendAddressResolver) : 
        this (backendAddressResolver.BackendAddress, new HttpClient())
    {
            
    }

    public void UpdateUrl (string url)
    {
        BaseUrl = url;
    }
}
