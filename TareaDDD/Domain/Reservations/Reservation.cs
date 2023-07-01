using Domain.Customers;
using Domain.Primitives;
using Domain.ValueObjects;
using Domain.TouristPackages;
using Domain.Reservations;

namespace Domain.Reservations;

public sealed class Reservation : AgregateRoot
{
    public Reservation(ReservationId id, CustomerId customerId, TouristPackageId touristPackageId, DateTime traveldate)
    {
        Id = id;
        CustomerId = customerId;
        TouristPackageId = touristPackageId;
        TravelDate = traveldate;
    }
    private Reservation()
    {

    }
    public ReservationId Id { get; private set; }
    public CustomerId CustomerId { get; private set; }
    public TouristPackageId TouristPackageId { get; private set; }
    public DateTime TravelDate { get; private set; }

    public static Reservation Create(CustomerId customerId, TouristPackageId touristPackageId, DateTime traveldate)
    {
        var reservation = new Reservation
        {
            Id = new ReservationId(Guid.NewGuid()),
            CustomerId = customerId,
            TouristPackageId = touristPackageId,
            TravelDate = traveldate
        };

        return reservation;
    }

    public void Update(CustomerId customerId, TouristPackageId touristPackageId, DateTime traveldate)
    {
        CustomerId = customerId;
        TouristPackageId = touristPackageId;
        TravelDate = traveldate;
    }

    public static Reservation UpdateReservation(
        Guid id,
        CustomerId customerId,
        TouristPackageId touristPackageId,
        DateTime traveldate)
    {
        return new Reservation(new ReservationId(id), customerId, touristPackageId, traveldate);
    }
}