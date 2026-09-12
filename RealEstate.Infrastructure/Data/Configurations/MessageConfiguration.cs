using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Data.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.SenderName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.SenderPhone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.SenderEmail)
            .HasMaxLength(150);

        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.IsRead)
            .IsRequired();

        builder.HasOne(x => x.property)
            .WithMany(x => x.messages)
            .HasForeignKey(x => x.propertyId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}