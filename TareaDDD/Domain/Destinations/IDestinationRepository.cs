namespace Domain.Destinations;

public interface IDestinationRepository
{
    Task<Destination?> GetByIdAsync(DestinationId id);
    Task<bool> ExistsAsync(DestinationId id);
    Task<List<Destination>> GetAll();

    void Add(Destination destination);

    void Update(Destination destination);

    void Remove(Destination destination);
    void Delete(Destination destination);
}