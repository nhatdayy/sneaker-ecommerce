using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SneakerECommerce.Application.InMemoryCache;
using SneakerECommerce.Application.Interfaces.ICachingService;
using SneakerECommerce.Application.Interfaces.IServices;
using SneakerECommerce.Application.Services;

namespace SneakerECommerce.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IJwtManager, JwtManager>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICachingService, CachingService>();
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(assembly));

            services.AddValidatorsFromAssembly(assembly);
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = "master";
            });
            return services;

        }
    }
}
