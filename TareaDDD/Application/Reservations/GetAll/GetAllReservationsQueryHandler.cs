using Application.Reservations.Common;
using Domain.Reservations;
using Domain.Destinations;
using Domain.Customers;
using Domain.TouristPackages;
using ErrorOr;
using MediatR;


namespace Application.Reservations.GetAll;

internal sealed class GetAllReservationsQueryHandler : IRequestHandler<GetAllReservationsQuery, ErrorOr<IReadOnlyList<ReservationResponse>>>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ITouristPackageRepository _touristPackageRepository;
    private readonly IDestinationRepository _destinationRepository;

    public GetAllReservationsQueryHandler(IReservationRepository reservationRepository, ICustomerRepository customerRepository, ITouristPackageRepository touristPackageRepository, IDestinationRepository destinationRepository)
    {
        _reservationRepository = reservationRepository;
        _customerRepository = customerRepository;
        _touristPackageRepository = touristPackageRepository;
        _destinationRepository = destinationRepository;
    }

    public async Task<ErrorOr<IReadOnlyList<ReservationResponse>>> Handle(GetAllReservationsQuery query, CancellationToken cancellationToken)
    {
        IReadOnlyList<Reservation> reservations = await _reservationRepository.GetAll();

        var responses = new List<ReservationResponse>();

        foreach (var reservation in reservations)
        {
            var customer = await _customerRepository.GetByIdAsync(reservation.CustomerId);
            var touristPackage = await _touristPackageRepository.GetByIdWithLineItemAsync(reservation.TouristPackageId);

            var lineItemResponses = new List<LineItemResponse>();

            foreach (var lineItem in touristPackage.LineItems)
            {
                var destination = await _destinationRepository.GetByIdAsync(lineItem.DestinationId);
                string Name = destination != null ? destination.Name : string.Empty;
                string Ubication = destination != null ? destination.Ubication : string.Empty;

                var lineItemResponse = new LineItemResponse(Name, Ubication);
                lineItemResponses.Add(lineItemResponse);
            }

            var response = new ReservationResponse(
                reservation.Id.Value,
                customer?.FullName ?? string.Empty,
                customer?.Email ?? string.Empty,
                customer?.PhoneNumber.Value ?? string.Empty,
                reservation.TravelDate,
                new TouristPackageResponse(
                    touristPackage?.Name ?? string.Empty,
                    lineItemResponses
                    )
            );

            responses.Add(response);
        }

        return responses;
    }

}