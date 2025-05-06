using Domain.Repositories;
using Infrastructure.Identity.Models;
using Infrastructure.Identity.Services;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Configuración de DbContext
            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => 
                    {
                        sqlOptions.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName);
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    }));

            // Registrar interceptores
            services.AddScoped<AuditableEntityInterceptor>();

            // Registrar repositorios
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();

            // Registrar servicios de seguridad
            services.AddSingleton<PasswordHasher>();
            services.AddScoped<TokenService>();

            // Configuración de HttpClient y logging
            services.AddHttpClient();
            services.AddLogging();
            
            // Configuración de Keycloak (si está habilitado)
            if (configuration.GetValue<bool>("UseKeycloak"))
            {
                services.Configure<KeycloakSettings>(
                    configuration.GetSection("KeycloakSettings"));
                services.AddScoped<KeycloakService>();
            }

            return services;
        }
    }
}