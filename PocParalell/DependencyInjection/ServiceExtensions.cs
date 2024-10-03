using Microsoft.EntityFrameworkCore;
using PocParalell.Infrastructure.Context;
using PocParalell.Infrastructure.Interfaces;
using PocParalell.Infrastructure.Repositories;
using PocParalell.Services;
using PocParalell.Services.Interfaces;

namespace PocParalell.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IInsereService, InsereService>();
            services.AddScoped<ICalculoService, CalculoService>();

            return services;
        }
    }
}
