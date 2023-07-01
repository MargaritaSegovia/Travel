using Application.TouristPackages.Common;
using Domain.TouristPackages;
using Domain.Destinations;
using ErrorOr;
using MediatR;

using Domain.Primitives;

namespace Application.TouristPackages.GetAll;


internal sealed class GetAllTouristPackagesQueryHandler : IRequestHandler<GetAllTouristPackagesQuery, ErrorOr<IReadOnlyList<TouristPackageResponse>>>
{
    private readonly ITouristPackageRepository _touristPackageRepository;
    private readonly IDestinationRepository _destinationRepository;
    private readonly IUnitOfWork _unitofwork;

    public GetAllTouristPackagesQueryHandler(ITouristPackageRepository touristPackageRepository, IDestinationRepository destinationRepository, IUnitOfWork unitofwork)
    {
        _touristPackageRepository = touristPackageRepository;
        _destinationRepository = destinationRepository;
        _unitofwork = unitofwork;
    }

    public async Task<ErrorOr<IReadOnlyList<TouristPackageResponse>>> Handle(GetAllTouristPackagesQuery query, CancellationToken cancellationToken)
    {
        IReadOnlyList<TouristPackage> touristPackages = await _touristPackageRepository.GetAll();

        var responses = new List<TouristPackageResponse>();

        foreach (var touristPackage in touristPackages)
        {
            var lineItemResponses = new List<LineItemResponse>();

            foreach (var lineItem in touristPackage.LineItems)
            {
                var destination = await _destinationRepository.GetByIdAsync(lineItem.DestinationId);
                string Name = destination != null ? destination.Name : string.Empty;
                string Ubication = destination != null ? destination.Ubication : string.Empty;

                var lineItemResponse = new LineItemResponse(Name, Ubication);
                lineItemResponses.Add(lineItemResponse);
            }

            var touristPackageResponse = new TouristPackageResponse(
                touristPackage.Id.Value,
                touristPackage.Name,
                touristPackage.Description,
                new MoneyResponse(
                    touristPackage.Price.Currency,
                    touristPackage.Price.Amount),
                touristPackage.TravelDate,
                lineItemResponses);

            responses.Add(touristPackageResponse);
        }

        return responses;
    }
}