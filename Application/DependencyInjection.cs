using Application.Abstraction;
using Application.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IDispatcher, Dispatcher>();
            return services;
        }
    }
}
