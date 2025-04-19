using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SneakerECommerce.Application.Interfaces.IServices;
using SneakerECommerce.Application.Services;

namespace SneakerECommerce.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IJwtManager, JwtManager>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IUserService, UserService>();
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(assembly));

            services.AddValidatorsFromAssembly(assembly);
            return services;
        }
    }
}
