using CafeConnect.Application.Features.Cafe.Dtos;
using CafeConnect.Domain.Repositories;
using MediatR;

namespace CafeConnect.Application.Features.Cafe.Queries
{
    public class GetAllCafesQuery : IRequest<IEnumerable<CafesByLocationDto>>
    {
        public string? Location { get; set; }
    }

    public class GetAllCafesQueryHandler : IRequestHandler<GetAllCafesQuery, IEnumerable<CafesByLocationDto>>
    {
        private readonly ICafeRepository _cafeRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IImageUploadRepository _imageUploadRepository;

        public GetAllCafesQueryHandler(ICafeRepository cafeRepository, IEmployeeRepository employeeRepository, IImageUploadRepository imageUploadRepository)
        {
            _cafeRepository = cafeRepository;
            _employeeRepository = employeeRepository;
            _imageUploadRepository = imageUploadRepository;
        }

        public async Task<IEnumerable<CafesByLocationDto>> Handle(GetAllCafesQuery request, CancellationToken cancellationToken)
        {
            // get cafes by location
            var cafes = await _cafeRepository.GetAllAsync(cafe => string.IsNullOrEmpty(request.Location) || cafe.Location.Contains(request.Location));

            // get employees by cafe
            var filteredCafeIds = cafes.Select(c => c.Id).Where(c => c != Guid.Empty).ToList();
            var employees = await _employeeRepository.GetAllAsync(emp => filteredCafeIds.Contains(emp.CafeId.Value));

            // get image info
            var filteredImageIds = cafes.Select(c => c.LogoId).ToList();
            var imageInfors = await _imageUploadRepository.GetAllAsync(imageUpload => filteredImageIds.Contains(imageUpload.Id));

            var query = cafes.Select(cafe => new CafesByLocationDto
            {
                CafeId = cafe.Id,
                Description = cafe.Description,
                Name = cafe.Name,
                Location = cafe.Location,
                LogoPath = imageInfors.FirstOrDefault(f => f.Id == cafe.LogoId)?.FilePath ?? string.Empty,
                EmployeeCount = employees.Count(c => c.CafeId == cafe.Id),
            });

            return query.OrderByDescending(o => o.EmployeeCount);
        }
    }
}