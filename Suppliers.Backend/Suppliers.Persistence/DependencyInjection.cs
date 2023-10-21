using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suppliers.Application.Interfaces;

namespace Suppliers.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration) 
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<SuppliersDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<ISuppliersDbContext>(provider => provider.GetService<SuppliersDbContext>()!);

            return services;
        }
    }
}
