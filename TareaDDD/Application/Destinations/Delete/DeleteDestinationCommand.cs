using ErrorOr;
using MediatR;

namespace Application.Destinations.Delete;

public record DeleteDestinationCommand(Guid Id) : IRequest<ErrorOr<Unit>>;