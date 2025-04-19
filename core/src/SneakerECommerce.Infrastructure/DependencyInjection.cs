
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sneaker_Ecommerce.Domain.Entity;
using Sneaker_ECommerce.Infrastructure.Data;
using SneakerECommerce.Application.Interfaces.IRepositories;
using SneakerECommerce.Infrastructure.Repositories;

namespace SneakerECommerce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository<User>, UserRepository>();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(DataContext).Assembly.FullName));
            });

            return services;
        }
    }
}
