using Domain.Reservations;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

internal sealed class ReservationRepository : IReservationRepository
{
    private readonly ApplicationDbContext _context;

    public ReservationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Reservation?> GetByIdAsync(ReservationId id) => await _context.Reservations.SingleOrDefaultAsync(p => p.Id == id);
    public async Task<List<Reservation>> GetAll() => await _context.Reservations.ToListAsync();
    public void Add(Reservation reservation) => _context.Reservations.Add(reservation);
    public void Update(Reservation reservation) => _context.Reservations.Update(reservation);
    public void Delete(Reservation reservation) => _context.Reservations.Remove(reservation);
    public async Task<bool> ExistsAsync(ReservationId id) => await _context.Reservations.AnyAsync(reservation => reservation.Id == id);
    public async Task<Reservation?> GetById(ReservationId id) => await _context.Reservations.SingleOrDefaultAsync(p => p.Id == id);

    public Task<Reservation?> GetByIdWithLineItemAsync(ReservationId id)
    {
        throw new NotImplementedException();
    }

    public bool HasOneLineItem(Reservation reservation)
    {
        throw new NotImplementedException();
    }
}