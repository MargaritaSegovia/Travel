using Application.Reservations.Common;
using Domain.Reservations;
using Domain.Destinations;
using Domain.Customers;
using Domain.TouristPackages;
using ErrorOr;
using MediatR;

namespace Application.Reservations.GetById;

internal sealed class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, ErrorOr<ReservationResponse>>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ITouristPackageRepository _touristPackageRepository;
    private readonly IDestinationRepository _destinationRepository;

    public GetReservationByIdQueryHandler(IReservationRepository reservationRepository, ICustomerRepository customerRepository, ITouristPackageRepository touristPackageRepository, IDestinationRepository destinationRepository)
    {
        _reservationRepository = reservationRepository;
        _customerRepository = customerRepository;
        _touristPackageRepository = touristPackageRepository;
        _destinationRepository = destinationRepository;
    }

    public async Task<ErrorOr<ReservationResponse>> Handle(GetReservationByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _reservationRepository.GetByIdAsync(new ReservationId(query.Id)) is not Reservation reservation)
        {
            return Error.NotFound("Reservation.NotFound", "The reservation with the provided Id was not found.");
        }

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

        return response;
    }
}
