using System.Linq.Expressions;
using CafeConnect.Domain.Entities;

namespace CafeConnect.Domain.Repositories
{
    public interface ICafeRepository
    {
        public Task<Guid> InsertAsync(Cafe entity);
        public Task DeleteAsync(Cafe entity);
        public Task UpdateAsync(Cafe entity);
        public Task<List<Cafe>> GetAllAsync(Expression<Func<Cafe, bool>>? filter = null);
        public Task<Cafe?> GetByIdAsync(Guid id);
    }
}