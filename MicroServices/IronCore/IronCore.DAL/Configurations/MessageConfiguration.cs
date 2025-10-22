using IronCore.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IronCore.DAL.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("Messages");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .ValueGeneratedOnAdd();

        builder.Property(m => m.Content)
            .HasMaxLength(4000);

        builder.Property(m => m.ChannelId)
            .IsRequired();

        builder.Property(m => m.SendedById)
            .IsRequired()
            .HasMaxLength(450);

        builder.Property(m => m.IsArchived)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(m => m.CreatedDate)
            .IsRequired();

        builder.Property(m => m.ArchiveDate)
            .IsRequired(false);

        builder.HasIndex(m => m.ChannelId);
        builder.HasIndex(m => m.SendedById);
        builder.HasIndex(m => m.StorageId);
        builder.HasIndex(m => m.CreatedDate);
        builder.HasIndex(m => m.IsArchived);

        builder.HasOne(m => m.Channel)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.ChannelId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.SendedBy)
            .WithMany()
            .HasForeignKey(m => m.SendedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Storage)
           .WithMany()
           .HasForeignKey(m => m.StorageId)
           .OnDelete(DeleteBehavior.Restrict);
    }
}
