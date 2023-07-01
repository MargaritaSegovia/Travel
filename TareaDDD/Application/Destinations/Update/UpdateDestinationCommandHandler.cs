
using Domain.Destinations;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Destinations.Update;

internal sealed class UpdateDestinationCommandHandler : IRequestHandler<UpdateDestinationCommand, ErrorOr<Unit>>
{
    private readonly IDestinationRepository _destinationRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateDestinationCommandHandler(IDestinationRepository destinationRepository, IUnitOfWork unitOfWork)
    {
        _destinationRepository = destinationRepository ?? throw new ArgumentNullException(nameof(destinationRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(UpdateDestinationCommand command, CancellationToken cancellationToken)
    {
        if (!await _destinationRepository.ExistsAsync(new DestinationId(command.Id)))
        {
            return Error.NotFound("Customer.NotFound", "The customer with the provide Id was not found.");
        }

        Destination destination = Destination.UpdateDestination(command.Id, command.Name, command.Description, command.Ubication);

        _destinationRepository.Update(destination);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}