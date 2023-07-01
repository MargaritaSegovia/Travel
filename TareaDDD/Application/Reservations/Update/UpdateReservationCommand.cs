using Domain.Customers;
using Domain.TouristPackages;
using ErrorOr;
using MediatR;

namespace Application.Reservations.Update;

public record UpdateReservationCommand(
    Guid Id,
    CustomerId CustomerId,
    TouristPackageId TouristPackageId,
    DateTime Traveldate
    ) : IRequest<ErrorOr<Unit>>;