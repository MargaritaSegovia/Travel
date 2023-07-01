using MediatR;
using ErrorOr;

namespace Application.Reservations.Common;

public record ReservationResponse(
    Guid Id,
    string Name,
    string Email,
    string PhoneNumber,
    DateTime DateTime,
    TouristPackageResponse LineItems
) : IRequest<ErrorOr<ReservationResponse>>;

public record TouristPackageResponse(
    string Nombre,
    List<LineItemResponse> Items
    );

public record LineItemResponse(
    string Nombre,
    string Ubicacion);

