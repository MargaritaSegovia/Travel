using Domain.Reservations;

namespace Domain.Reservations;

public interface IReservationRepository
{

    Task<Reservation?> GetByIdWithLineItemAsync(ReservationId id);
    bool HasOneLineItem(Reservation reservation);
    Task<List<Reservation>> GetAll();
    Task<Reservation?> GetByIdAsync(ReservationId id);
    void Add(Reservation reservation);
    Task<bool> ExistsAsync(ReservationId id);
    void Update(Reservation reservation);
    void Delete(Reservation reservation);
}