using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Data.Configurations;

public class AgentConfiguration : IEntityTypeConfiguration<Agent>
{
    public void Configure(EntityTypeBuilder<Agent> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Phone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(a => a.WhatsApp)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(a => a.Email)
            .IsRequired()
            .HasMaxLength(150);
    }
}