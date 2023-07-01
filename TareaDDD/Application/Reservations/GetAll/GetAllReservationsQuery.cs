using ErrorOr;
using MediatR;
using Application.Reservations.Common;

namespace Application.Reservations.GetAll;

public record GetAllReservationsQuery() : IRequest<ErrorOr<IReadOnlyList<ReservationResponse>>>;