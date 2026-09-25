using System;
using Microsoft.Extensions.DependencyInjection;

namespace manufacturingonaspdotnet.Service;

public class ServiceResolver : IServiceResolver
{
    private readonly IServiceProvider serviceProvider;

    public ServiceResolver(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public T Get<T>() where T : notnull
    {
        return serviceProvider.GetRequiredService<T>();
    }
}