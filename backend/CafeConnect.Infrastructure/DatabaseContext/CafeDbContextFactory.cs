using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CafeConnect.Infrastructure.DatabaseContext
{
    public class CafeDbContextFactory : IDesignTimeDbContextFactory<CafeDbContext>
    {
        public CafeDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<CafeDbContext>();
            optionsBuilder.UseMySql(
                    connectionString, ServerVersion.AutoDetect(connectionString),
                    builder => builder.MigrationsAssembly(typeof(CafeDbContext).Assembly.FullName));

            return new CafeDbContext(optionsBuilder.Options);
        }
    }
}