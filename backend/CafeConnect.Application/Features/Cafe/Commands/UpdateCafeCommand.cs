using CafeConnect.Domain.Exceptions;
using CafeConnect.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CafeConnect.Application.Features.Cafe.Commands
{
    public class UpdateCafeCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IFormFile? Logo { get; set; } = null;
        public string Location { get; set; } = string.Empty;
    }

    public class UpdateCafeCommandHandler : IRequestHandler<UpdateCafeCommand>
    {
        private readonly ICafeRepository _cafeRepository;
        private readonly IImageUploadRepository _imgUploadRepository;

        public UpdateCafeCommandHandler(ICafeRepository cafeRepository, IImageUploadRepository imgUploadRepository)
        {
            _cafeRepository = cafeRepository;
            _imgUploadRepository = imgUploadRepository;
        }

        public async Task Handle(UpdateCafeCommand request, CancellationToken cancellationToken)
        {
            var item = await _cafeRepository.GetByIdAsync(request.Id) ?? throw new ItemNotFoundException(nameof(Cafe), request.Id); ;
            var img = item.LogoId.HasValue ? await _imgUploadRepository.GetByIdAsync(item.LogoId.Value) : null;

            Guid? imageId = null;
            if (request.Logo != null && request.Logo.FileName != img?.FileName) // upload if the image is difference (TODO: checksum)
                imageId = await _imgUploadRepository.UploadFileAsync(request.Logo);

            item.LogoId = imageId ?? item.LogoId;
            item.Name = request.Name;
            item.Location = request.Location;
            item.Description = request.Description;

            await _cafeRepository.UpdateAsync(item);
        }
    }
}