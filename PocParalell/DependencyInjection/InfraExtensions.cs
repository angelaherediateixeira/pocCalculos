using Microsoft.EntityFrameworkCore;
using PocParalell.Infrastructure.Context;
using PocParalell.Infrastructure.Interfaces;
using PocParalell.Infrastructure.Repositories;
using System.Configuration;

namespace PocParalell.DependencyInjection
{
    public static class InfraExtensions
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICdiRepository, CdiRepository>();
            services.AddScoped<IPosicaoRepository, PosicaoRepository>();
            services.AddScoped<ICalculoRepository, CalculoRepository>();
            services.AddScoped<IExecucaoRepository, ExecucaoRepository>();

            services.AddDbContext<SqlEntityFrameworkDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlServer")),
                ServiceLifetime.Transient);

            return services;
        }
    }
}
