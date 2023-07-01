
using Domain.Destinations;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Destinations.Delete;

internal sealed class DeleteDestinationCommandHandler : IRequestHandler<DeleteDestinationCommand, ErrorOr<Unit>>
{
    private readonly IDestinationRepository _destinationRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteDestinationCommandHandler(IDestinationRepository destinationRepository, IUnitOfWork unitOfWork)
    {
        _destinationRepository = destinationRepository ?? throw new ArgumentNullException(nameof(destinationRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(DeleteDestinationCommand command, CancellationToken cancellationToken)
    {
        if (await _destinationRepository.GetByIdAsync(new DestinationId(command.Id)) is not Destination destination)
        {
            return Error.NotFound("Destination.NotFound", "The destination with the provide Id was not found.");
        }

        _destinationRepository.Remove(destination);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}