using System.Linq.Expressions;
using CafeConnect.Domain.Entities;
using CafeConnect.Domain.Exceptions;
using CafeConnect.Domain.Repositories;
using CafeConnect.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CafeConnect.Infrastructure.Repositories
{
    public class CafeRepository : ICafeRepository
    {
        private readonly CafeDbContext _context;

        public CafeRepository(CafeDbContext context) => _context = context;

        public async Task<Guid> InsertAsync(Cafe entity)
        {
            _context.Cafes.Add(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task DeleteAsync(Cafe entity)
        {
            var item = await _context.Cafes.FirstOrDefaultAsync(c => c.Id == entity.Id) ?? throw new ItemNotFoundException(nameof(entity), entity.Id);

            var employees = await _context.Employees.Where(emp => emp.CafeId == entity.Id).ToListAsync();

            _context.Employees.RemoveRange(employees);
            _context.Cafes.Remove(entity);

            // move to history tables
            await _context.CafeHistories.AddAsync(new CafeHistory
            {
                Id = Guid.NewGuid(),
                CafeId = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Location = entity.Location,
                LogoId = entity.LogoId,
                Employees = employees.Select(emp => new EmployeeHistory
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = emp.Id,
                    CafeId = emp.CafeId,
                    Name = emp.Name,
                    EmailAddress = emp.EmailAddress,
                    PhoneNumber = emp.PhoneNumber,
                    Gender = emp.Gender,
                    StartedAt = emp.StartedAt,
                }).ToList()
            });

            await _context.SaveChangesAsync();
        }

        public async Task<List<Cafe>> GetAllAsync(Expression<Func<Cafe, bool>>? filter = null) => (filter == null) ?
             await _context.Cafes.ToListAsync() :
             await _context.Cafes.Where(filter).ToListAsync();

        public async Task<Cafe?> GetByIdAsync(Guid id) => await _context.Cafes.SingleOrDefaultAsync(c => c.Id == id);

        public async Task UpdateAsync(Cafe entity)
        {
            var item = await _context.Cafes.FirstOrDefaultAsync(c => c.Id == entity.Id) ?? throw new ItemNotFoundException(nameof(entity), entity.Id);

            item.Name = entity.Name;
            item.Location = entity.Location;
            item.Description = entity.Description;

            await _context.SaveChangesAsync();
        }
    }
}