using CafeConnect.Application.Features.Cafe.Dtos;
using CafeConnect.Domain.Repositories;
using MediatR;

namespace CafeConnect.Application.Features.Cafe.Queries
{
    public class GetCafeQuery : IRequest<CafeDto?>
    {
        public Guid CafeId { get; set; }
    }

    public class GetCafeQueryHandler : IRequestHandler<GetCafeQuery, CafeDto?>
    {
        private readonly ICafeRepository _cafeRepository;
        private readonly IImageUploadRepository _imageUploadRepository;

        public GetCafeQueryHandler(ICafeRepository cafeRepository, IImageUploadRepository imageUploadRepository)
        {
            _cafeRepository = cafeRepository;
            _imageUploadRepository = imageUploadRepository;
        }

        public async Task<CafeDto?> Handle(GetCafeQuery request, CancellationToken cancellationToken)
        {
            var item = await _cafeRepository.GetByIdAsync(request.CafeId);
            if (item == null) return null;

            return new CafeDto
            {
                CafeId = item.Id,
                Name = item.Name,
                Description = item.Description,
                Location = item.Location,
                LogoPath = (await _imageUploadRepository.GetByIdAsync(item.LogoId.Value))?.FilePath ?? string.Empty,
            };
        }
    }
}