using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendClient;
public partial class CryptoBackClient
{
    public CryptoBackClient() : this ("https://localhost:32775", new HttpClient())
    {
            
    }
}
