using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;

namespace CafeConnect.Domain.Repositories
{
    public interface IImageUploadRepository
    {
        Task<Guid> UploadFileAsync(IFormFile file);

        Task<List<Entities.FileInfo>> GetAllAsync(Expression<Func<Entities.FileInfo, bool>> filter = null);

        Task<Entities.FileInfo?> GetByIdAsync(Guid Id);
    }
}