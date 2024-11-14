using System.Reflection;
using CafeConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CafeConnect.Infrastructure.DatabaseContext
{
    public class CafeDbContext : DbContext
    {
        public DbSet<Cafe> Cafes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<CafeHistory> CafeHistories { get; set; }
        public DbSet<EmployeeHistory> EmployeeHistories { get; set; }
        public DbSet<Domain.Entities.FileInfo> FileInfos { get; set; }

        public CafeDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}