using System.Linq.Expressions;
using CafeConnect.Domain.Entities;

namespace CafeConnect.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        public Task<string> InsertAsync(Employee entity);
        public Task DeleteAsync(Employee entity);
        public Task UpdateAsync(Employee entity);
        public Task<List<Employee>> GetAllAsync(Expression<Func<Employee, bool>>? filter = null);
        public Task<Employee?> GetByIdAsync(string id);
    }
}