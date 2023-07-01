using Application.Reservations.Common;
using ErrorOr;
using MediatR;

namespace Application.Reservations.GetById;

public record GetReservationByIdQuery(Guid Id) : IRequest<ErrorOr<ReservationResponse>>;