using Domain.Destinations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class DestinationConfiguration : IEntityTypeConfiguration<Destination>
{
    public void Configure(EntityTypeBuilder<Destination> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasConversion(
            destinationId => destinationId.Value,
            value => new DestinationId(value));

        builder.Property(c => c.Name).HasMaxLength(30);
        builder.Property(c => c.Description).HasMaxLength(50);
        builder.Property(c => c.Ubication).HasMaxLength(50);
    }
}