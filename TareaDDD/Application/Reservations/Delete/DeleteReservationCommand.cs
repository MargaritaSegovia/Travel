using ErrorOr;
using MediatR;

namespace Application.Reservations.Delete;

public record DeleteReservationCommand(Guid Id) : IRequest<ErrorOr<Unit>>;