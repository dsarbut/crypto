using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendClient;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection UseBackendClient (this IServiceCollection  builder)
    {
        return builder.AddSingleton<CryptoBackClient>();
    }
}
