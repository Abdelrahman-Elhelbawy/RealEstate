using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Data.Configurations;

public class PropertyReportConfiguration : IEntityTypeConfiguration<PropertyReport>
{
    public void Configure(EntityTypeBuilder<PropertyReport> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Reason)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.ReporterName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.ReporterPhone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.IsResolved)
            .IsRequired();

        builder.HasOne(x => x.property)
            .WithMany(x => x.PropertyReports)
            .HasForeignKey(x => x.propertyId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}