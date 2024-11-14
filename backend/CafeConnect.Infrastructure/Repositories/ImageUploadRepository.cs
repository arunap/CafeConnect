using System.Linq.Expressions;
using CafeConnect.Domain.Repositories;
using CafeConnect.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CafeConnect.Infrastructure.Repositories
{
    public class ImageUploadRepository : IImageUploadRepository
    {
        private readonly CafeDbContext _context;
        private readonly string _fileUploadPath;

        public ImageUploadRepository(CafeDbContext context, IConfiguration configuration)
        {
            _context = context;

            _fileUploadPath = configuration["ImageUploadPath"] ?? Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            Directory.CreateDirectory(_fileUploadPath);
        }

        public async Task<List<Domain.Entities.FileInfo>> GetAllAsync(Expression<Func<Domain.Entities.FileInfo, bool>> filter = null)
        {
            return filter == null ? await _context.FileInfos.ToListAsync() : await _context.FileInfos.Where(filter).ToListAsync();
        }

        public async Task<Domain.Entities.FileInfo?> GetByIdAsync(Guid Id) => await _context.FileInfos.SingleOrDefaultAsync(f => f.Id == Id);

        public async Task<Guid> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0) throw new ArgumentException("Invalid file");

            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(_fileUploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            Guid newId = Guid.NewGuid();
            _context.FileInfos.Add(new Domain.Entities.FileInfo
            {
                Id = newId,
                ContentType = file.ContentType,
                FileName = file.FileName,
                FilePath = filePath,
                FileSize = file.Length / 1024,
                UploadedAt = DateTime.Now
            });

            await _context.SaveChangesAsync();

            return newId;
        }
    }
}