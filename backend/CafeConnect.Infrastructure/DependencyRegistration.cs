using CafeConnect.Domain.Repositories;
using CafeConnect.Infrastructure.DatabaseContext;
using CafeConnect.Infrastructure.Repositories;
using CafeConnect.Infrastructure.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CafeConnect.Infrastructure
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<ICafeRepository, CafeRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IImageUploadRepository, ImageUploadRepository>();

            services.AddScoped<CafeDataInitializer>();
            services.AddScoped<EmployeeDataInitializer>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<CafeDbContext>(
                options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                builder => builder.MigrationsAssembly(typeof(CafeDbContext).Assembly.FullName)
            ));

            services.AddSingleton<IDesignTimeDbContextFactory<CafeDbContext>, CafeDbContextFactory>();

            return services;
        }

        public static async Task SeedAsync(this IServiceProvider serviceProvider)
        {
            using var sp = serviceProvider.CreateScope();
            var cafeSeeds = sp.ServiceProvider.GetRequiredService<CafeDataInitializer>();
            await cafeSeeds.SeedAsync();

            var employeeSeeds = sp.ServiceProvider.GetRequiredService<EmployeeDataInitializer>();
            await employeeSeeds.SeedAsync();
        }
    }
}