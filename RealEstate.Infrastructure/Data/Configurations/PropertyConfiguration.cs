using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Data.Configurations;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Description)
            .IsRequired(false)
            .HasMaxLength(2000);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(p => p.PropertyType)
            .IsRequired();

        builder.Property(p => p.TransactionType)
            .IsRequired();

        builder.Property(p => p.Area)
            .IsRequired();

        builder.Property(p => p.Bedrooms)
            .IsRequired();

        builder.Property(p => p.Bathrooms)
            .IsRequired();

        builder.Property(p => p.Floor)
            .IsRequired();

        builder.Property(p => p.Furnished)
            .IsRequired();

        builder.Property(p => p.Address)
            .IsRequired(false)
            .HasMaxLength(300);

        builder.Property(p => p.City)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(p => p.Latitude)
            .IsRequired();

        builder.Property(p => p.Longitude)
            .IsRequired();

        builder.Property(p => p.Status)
            .IsRequired();

        builder.Property(p => p.IsFeatured)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(p => p.ViewsCount)
            .IsRequired()
            .HasDefaultValue(0);

        builder.HasOne(p => p.agent)
            .WithMany(a => a.properties)
            .HasForeignKey(p => p.agentId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}