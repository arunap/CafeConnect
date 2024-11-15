using System.Linq.Expressions;
using CafeConnect.Domain.Repositories;
using CafeConnect.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace CafeConnect.Infrastructure.Repositories
{
    public class ImageUploadRepository : IImageUploadRepository
    {
        private readonly CafeDbContext _context;
        private readonly string _fileUploadPath;
        private readonly string _imageUploadPath;

        public ImageUploadRepository(CafeDbContext context, IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            _context = context;

            _imageUploadPath = configuration["ImageUploadPath"] ?? "Uploads\\Images";
            _fileUploadPath = Path.Combine(hostEnvironment.ContentRootPath, "wwwroot", _imageUploadPath);
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
            string serverImgPath = Path.Combine(_imageUploadPath, fileName);

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
                FilePath = serverImgPath,
                FileSize = file.Length / 1024,
                UploadedAt = DateTime.Now
            });

            await _context.SaveChangesAsync();

            return newId;
        }
    }
}