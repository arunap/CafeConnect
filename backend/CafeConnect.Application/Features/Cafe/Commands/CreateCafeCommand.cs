using CafeConnect.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CafeConnect.Application.Features.Cafe.Commands
{
    public class CreateCafeCommand : IRequest<Guid>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        [Microsoft.AspNetCore.Mvc.FromForm]
        public IFormFile? Logo { get; set; } = null;
        public string Location { get; set; } = null!;
    }

    public class CreateCafeCommandHandler : IRequestHandler<CreateCafeCommand, Guid>
    {
        private readonly ICafeRepository _cafeRepository;
        private readonly IImageUploadRepository _imageUploadRepository;

        public CreateCafeCommandHandler(ICafeRepository cafeRepository, IImageUploadRepository imageUploadRepository)
        {
            _cafeRepository = cafeRepository;
            _imageUploadRepository = imageUploadRepository;
        }

        public async Task<Guid> Handle(CreateCafeCommand request, CancellationToken cancellationToken)
        {
            Guid? imageId = null;
            if (request.Logo != null) imageId = await _imageUploadRepository.UploadFileAsync(request.Logo);

            var cafe = new Domain.Entities.Cafe
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                LogoId = imageId,
                Location = request.Location
            };

            Guid insertedId = await _cafeRepository.InsertAsync(cafe);

            return insertedId;
        }
    }
}