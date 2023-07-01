using MediatR;
using ErrorOr;

namespace Application.Destinations;

public record CreateDestinationCommand(
    string Name,
    string Description,
    string Ubication
) : IRequest<ErrorOr<Unit>>;