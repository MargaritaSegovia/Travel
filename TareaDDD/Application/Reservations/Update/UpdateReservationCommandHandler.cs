using Domain.Customers;
using Domain.TouristPackages;
using Domain.Reservations;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Reservations.Update;
public sealed class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, ErrorOr<Unit>>
{

    private readonly IReservationRepository _reservationRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ITouristPackageRepository _touristPackageRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateReservationCommandHandler(IReservationRepository reservationRepository, ICustomerRepository customerRepository, ITouristPackageRepository touristPackageRepository, IUnitOfWork unitOfWork)
    {
        _reservationRepository = reservationRepository;
        _customerRepository = customerRepository;
        _touristPackageRepository = touristPackageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateReservationCommand command, CancellationToken cancellationToken)
    {
        if (!await _reservationRepository.ExistsAsync(new ReservationId(command.Id)))
        {
            return Error.NotFound("Customer.NotFound", "The customer with the provide Id was not found.");
        }

        Reservation reservation = Reservation.UpdateReservation(command.Id, command.CustomerId, command.TouristPackageId, command.Traveldate);

        _reservationRepository.Update(reservation);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}