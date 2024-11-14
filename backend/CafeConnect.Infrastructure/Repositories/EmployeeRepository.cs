using System.Linq.Expressions;
using CafeConnect.Domain.Entities;
using CafeConnect.Domain.Exceptions;
using CafeConnect.Domain.Repositories;
using CafeConnect.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CafeConnect.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CafeDbContext _context;

        public EmployeeRepository(CafeDbContext context) => _context = context;

        public async Task<string> InsertAsync(Employee entity)
        {
            await _context.Employees.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task DeleteAsync(Employee entity)
        {
            var item = await _context.Employees.FirstOrDefaultAsync(e => e.Id == entity.Id) ?? throw new ItemNotFoundException(nameof(Employee), entity.Id);

            _context.Employees.Remove(item);

            // move to history tables
            _context.EmployeeHistories.Add(new EmployeeHistory
            {
                Id = Guid.NewGuid(),
                EmployeeId = item.Id,
                CafeId = item.CafeId,
                Name = item.Name,
                EmailAddress = item.EmailAddress,
                PhoneNumber = item.PhoneNumber,
                Gender = item.Gender,
                StartedAt = item.StartedAt,
            });

            await _context.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetAllAsync(Expression<Func<Employee, bool>>? filter = null) => filter == null ?
            await _context.Employees.ToListAsync() :
            await _context.Employees.Where(filter).ToListAsync();

        public async Task<Employee?> GetByIdAsync(string id) => await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);

        public async Task UpdateAsync(Employee entity)
        {
            var item = await _context.Employees.FirstOrDefaultAsync(e => e.Id == entity.Id) ?? throw new ItemNotFoundException(nameof(Employee), entity.Id);

            item.Name = entity.Name;
            item.EmailAddress = entity.EmailAddress;
            item.PhoneNumber = entity.PhoneNumber;
            item.Gender = entity.Gender;
            item.CafeId = entity.CafeId;

            await _context.SaveChangesAsync();
        }
    }
}