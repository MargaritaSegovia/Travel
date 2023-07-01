using Domain.Destinations;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

internal sealed class DestinationRepository : IDestinationRepository
{
    private readonly ApplicationDbContext _context;

    public DestinationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Destination>> GetAll() => await _context.Destinations.ToListAsync();
    public async Task<Destination?> GetById(DestinationId id) => await _context.Destinations.SingleOrDefaultAsync(p => p.Id == id);
    public async Task<Destination?> GetByIdAsync(DestinationId id) => await _context.Destinations.SingleOrDefaultAsync(p => p.Id == id);
    public async Task<bool> ExistsAsync(DestinationId id) => await _context.Destinations.AnyAsync(destination => destination.Id == id);
    public void Delete(Destination destination) => _context.Destinations.Remove(destination);


    public void Add(Destination destination) => _context.Destinations.Add(destination);
    public void Update(Destination destination) => _context.Destinations.Update(destination);
    public void Remove(Destination destination) => _context.Destinations.Remove(destination);
}