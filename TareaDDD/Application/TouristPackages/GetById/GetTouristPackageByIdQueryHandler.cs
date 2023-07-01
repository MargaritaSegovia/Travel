using Application.TouristPackages.Common;
using Domain.TouristPackages;
using Domain.Destinations;
using ErrorOr;
using MediatR;

using Domain.Primitives;
using Application.TouristPackages.GetById;

namespace Application.TouristPackages.GetAll;


internal sealed class GetTouristPackageByIdQueryHandler : IRequestHandler<GetTouristPackageByIdQuery, ErrorOr<TouristPackageResponse>>
{
    private readonly ITouristPackageRepository _touristPackageRepository;
    private readonly IDestinationRepository _destinationRepository;
    private readonly IUnitOfWork _unitofwork;

    public GetTouristPackageByIdQueryHandler(ITouristPackageRepository touristPackageRepository, IDestinationRepository destinationRepository, IUnitOfWork unitofwork)
    {
        _touristPackageRepository = touristPackageRepository;
        _destinationRepository = destinationRepository;
        _unitofwork = unitofwork;
    }

    public async Task<ErrorOr<TouristPackageResponse>> Handle(GetTouristPackageByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _touristPackageRepository.GetByIdAsync(new TouristPackageId(query.Id)) is not TouristPackage touristPackage)
        {
            return Error.NotFound("Customer.NotFound", "The customer with the provided Id was not found.");
        }

        var touristPackageResponse = new TouristPackageResponse(
            touristPackage.Id.Value,
            touristPackage.Name,
            touristPackage.Description,
            new MoneyResponse(
                touristPackage.Price.Currency,
                touristPackage.Price.Amount),
            touristPackage.TravelDate,
            new List<LineItemResponse>());

        foreach (var lineItem in touristPackage.LineItems)
        {
            var destination = await _destinationRepository.GetByIdAsync(lineItem.DestinationId);
            string name = destination != null ? destination.Name : string.Empty;
            string ubication = destination != null ? destination.Ubication : string.Empty;

            var lineItemResponse = new LineItemResponse(name, ubication);
            touristPackageResponse.LineItems.Add(lineItemResponse);
        }

        return touristPackageResponse;
    }



}