using ErrorOr;
using MediatR;

namespace Application.Destinations.Update;

public record UpdateDestinationCommand(
    Guid Id,
    string Name,
    string Description,
    string Ubication
    ) : IRequest<ErrorOr<Unit>>;