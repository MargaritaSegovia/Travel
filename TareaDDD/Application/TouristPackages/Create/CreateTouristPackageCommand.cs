using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.TouristPackages.Create;

public record CreateTouristPackageCommand(
    string Name,
    string Description,
    DateTime Traveldate,
    Money Price,
    List<CreateLineItemCommand> Items
    ) : IRequest<ErrorOr<Unit>>;

public record CreateLineItemCommand(Guid DestinationId)
{

}
