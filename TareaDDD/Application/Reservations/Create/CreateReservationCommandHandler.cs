using Domain.Customers;
using Domain.Reservations;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Reservations.Create;
public sealed class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, ErrorOr<Unit>>
{

    private readonly IReservationRepository _reservationRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitofwork;
    public CreateReservationCommandHandler(IReservationRepository reservationRepository, IUnitOfWork unitofwork, ICustomerRepository customerRepository)
    {

        _reservationRepository = reservationRepository;
        _unitofwork = unitofwork;
        _customerRepository = customerRepository;
    }


    public async Task<ErrorOr<Unit>> Handle(CreateReservationCommand command, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(new CustomerId(command.CustomerId));
        if (customer is null)
        {
            return Error.NotFound("Customer.NotFound", $"Customer with the Id {command.CustomerId} not exist");
        }
        var reservation = Reservation.Create(customer.Id, command.TouristPackageId, command.Traveldate);

        _reservationRepository.Add(reservation);

        await _unitofwork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}