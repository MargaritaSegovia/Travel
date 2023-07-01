using Domain.TouristPackages;
using ErrorOr;
using MediatR;

namespace Application.Reservations.Create;

public record CreateReservationCommand(
    Guid CustomerId,
    TouristPackageId TouristPackageId,
    DateTime Traveldate
    ) : IRequest<ErrorOr<Unit>>;