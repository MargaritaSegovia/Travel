using Domain.ValueObjects;

namespace Domain.Destinations;

public sealed class Destination
{
    public Destination(DestinationId id, string name, string description, string ubication)
    {
        Id = id;
        Name = name;
        Description = description;
        Ubication = ubication;
    }

    private Destination()
    {

    }

    public DestinationId Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Ubication { get; private set; } = string.Empty;


    public void Update(string name, string description, string ubication)
    {
        Name = name;
        Description = description;
        Ubication = ubication;
    }

    public static Destination UpdateDestination(Guid id, string name, string description, string ubication)
    {
        return new Destination(new DestinationId(id), name, description, ubication);
    }
}