using Domain.Destinations;
namespace Domain.TouristPackages;

public sealed class LineItem
{
    internal LineItem(LineItemId id, TouristPackageId touristPackageId, DestinationId destinationId)
    {
        Id = id;
        TouristPackageId = touristPackageId;
        DestinationId = destinationId;
    }

    private LineItem()
    {

    }

    public LineItemId Id { get; private set; }
    public TouristPackageId TouristPackageId { get; private set; }
    public DestinationId DestinationId { get; private set; }
    public Guid DestinationIdValue => DestinationId.Value;

    public void Update(DestinationId destinationId)
    {
        DestinationId = destinationId;
    }
}