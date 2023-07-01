namespace Application.Destinations.Common;


public record DestinationResponse(Guid Id,
    string name,
    string description,
    string ubication);

