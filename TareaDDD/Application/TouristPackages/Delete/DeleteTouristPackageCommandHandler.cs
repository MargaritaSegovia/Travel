using Domain.Reservations;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Reservations.Delete;

internal sealed class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand, ErrorOr<Unit>>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteReservationCommandHandler(IReservationRepository reservationRepository, IUnitOfWork unitOfWork)
    {
        _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(DeleteReservationCommand command, CancellationToken cancellationToken)
    {
        if (await _reservationRepository.GetByIdAsync(new ReservationId(command.Id)) is not Reservation reservation)
        {
            return Error.NotFound("Reservation.NotFound", "The reservation with the provide Id was not found.");
        }

        _reservationRepository.Delete(reservation);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
