using CafeConnect.Domain.Repositories;
using MediatR;

namespace CafeConnect.Application.Features.Cafe.Commands
{
    public class DeleteCafeCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteCafeCommandHandler : IRequestHandler<DeleteCafeCommand>
    {
        private readonly ICafeRepository _cafeRepository;

        public DeleteCafeCommandHandler(ICafeRepository cafeRepository)
        {
            _cafeRepository = cafeRepository;
        }

        public async Task Handle(DeleteCafeCommand request, CancellationToken cancellationToken)
        {
            await _cafeRepository.DeleteAsync(new Domain.Entities.Cafe { Id = request.Id });
        }
    }
}