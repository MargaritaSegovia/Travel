using Application.Destinations.Common;
using Domain.Destinations;
using ErrorOr;
using MediatR;

namespace Application.Destinations.GetById;


internal sealed class GetDestinationByIdQueryHandler : IRequestHandler<GetDestinationByIdQuery, ErrorOr<DestinationResponse>>
{
    private readonly IDestinationRepository _destinationRepository;

    public GetDestinationByIdQueryHandler(IDestinationRepository destinationRepository)
    {
        _destinationRepository = destinationRepository ?? throw new ArgumentNullException(nameof(destinationRepository));
    }

    public async Task<ErrorOr<DestinationResponse>> Handle(GetDestinationByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _destinationRepository.GetByIdAsync(new DestinationId(query.Id)) is not Destination Destination)
        {
            return Error.NotFound("Destination.NotFound", "The Destination with the provide Id was not found.");
        }

        return new DestinationResponse(
            Destination.Id.Value,
            Destination.Name,
            Destination.Description,
            Destination.Ubication
            );
    }
}