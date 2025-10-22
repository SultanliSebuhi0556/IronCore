using IronCore.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IronCore.DAL.Configurations;

public class ChannelConfiguration : IEntityTypeConfiguration<Channel>
{
    public void Configure(EntityTypeBuilder<Channel> builder)
    {
        builder.ToTable("Channels");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

        builder.Property(c => c.IsDirect)
                .IsRequired();

        builder.Property(c => c.IsArchived)
                .IsRequired()
                .HasDefaultValue(false);

        builder.Property(c => c.CreatedDate)
                .IsRequired();

        builder.Property(c => c.ArchiveDate)
                .IsRequired(false);

        builder.HasIndex(c => c.IsDirect);
        builder.HasIndex(c => c.IsArchived);
        builder.HasIndex(c => c.CreatedDate);

        builder.HasMany(c => c.Messages)
            .WithOne(m => m.Channel)
            .HasForeignKey(m => m.ChannelId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.ChannelUsers)
                .WithOne(cu => cu.Channel)
                .HasForeignKey(cu => cu.ChannelId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}
