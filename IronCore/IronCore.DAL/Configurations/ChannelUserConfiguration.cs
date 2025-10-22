using IronCore.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IronCore.DAL.Configurations;

public class ChannelUserConfiguration : IEntityTypeConfiguration<ChannelUser>
{
    public void Configure(EntityTypeBuilder<ChannelUser> builder)
    {
        builder.ToTable("ChannelUsers");

        builder.HasKey(cu => new { cu.ChannelId, cu.AppUserId });

        builder.Property(cu => cu.ChannelId)
            .IsRequired();

        builder.Property(cu => cu.AppUserId)
            .IsRequired()
            .HasMaxLength(450);

        builder.Property(cu => cu.JoinedDate)
            .IsRequired();

        builder.HasIndex(cu => cu.AppUserId);
        builder.HasIndex(cu => cu.JoinedDate);

        builder.HasOne(cu => cu.Channel)
            .WithMany(c => c.ChannelUsers)
            .HasForeignKey(cu => cu.ChannelId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cu => cu.AppUser)
            .WithMany(u => u.ChannelUsers)
            .HasForeignKey(cu => cu.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
