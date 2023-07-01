using Domain.TouristPackages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class TouristPackageConfiguration : IEntityTypeConfiguration<TouristPackage>
{

    public void Configure(EntityTypeBuilder<TouristPackage> builder)
    {

        builder.ToTable("TouristPackage");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasConversion(
            TouristPackageId => TouristPackageId.Value,
            value => new TouristPackageId(value)
        );
        builder.Property(c => c.Name).HasMaxLength(30);
        builder.Property(c => c.Description).HasMaxLength(50);

        builder.OwnsOne(c => c.Price, priceBuilder => { priceBuilder.Property(m => m.Currency).HasMaxLength(3); });

        builder.HasMany(o => o.LineItems)
            .WithOne().HasForeignKey(li => li.TouristPackageId);
    }
}