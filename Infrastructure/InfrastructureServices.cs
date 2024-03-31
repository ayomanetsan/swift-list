using Application.Common.Interfaces;
using AspNetCoreRateLimit;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(connectionString, opt => opt.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name)) );

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
            services.AddScoped<ILabelRepository, LabelRepository>();
            services.AddTransient<IPasswordManager, PasswordManager>();
            services.AddTransient<IJwtProvider, JwtProvider>();

            return services;
        }

        public static IServiceCollection AddIpRateLimiting(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            services.AddInMemoryRateLimiting();

            return services;
        }

        public static void UseCoreContext(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            using var context = scope?.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context?.Database.Migrate();
        }
    }
}
