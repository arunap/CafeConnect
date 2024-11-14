using CafeConnect.Domain.Repositories;
using CafeConnect.Infrastructure.DatabaseContext;
using CafeConnect.Infrastructure.Repositories;
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

            // serviceCollection.AddScoped<IDateTimeProvider, DateTimeProvider>();

            // database initial data initializer registration
            // serviceCollection.AddScoped<CafeDataInitializer>();
            // serviceCollection.AddScoped<EmployeeDataInitializer>();


            // get connectionstring
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Configure shared CafeManagementDbContext
            services.AddDbContext<CafeDbContext>(
                options => options.UseSqlServer(connectionString,
                builder => builder.MigrationsAssembly(typeof(CafeDbContext).Assembly.FullName)
            ));

            //  serviceCollection.AddScoped<ICafeDbContext>(provider => provider.GetRequiredService<CafeDbContext>());
            services.AddSingleton<IDesignTimeDbContextFactory<CafeDbContext>, CafeDbContextFactory>();

            return services;
        }
    }
}