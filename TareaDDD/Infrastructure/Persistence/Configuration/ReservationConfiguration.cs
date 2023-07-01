using Domain.Customers;
using Domain.Reservations;
using Domain.TouristPackages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{

    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("Reservations");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasConversion(
            ReservationId => ReservationId.Value,
            value => new ReservationId(value)
        );

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .IsRequired();

        builder.HasOne<TouristPackage>()
        .WithMany()
        .HasForeignKey(o => o.TouristPackageId);
    }
}