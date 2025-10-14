using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcAsCloud.CORE.Entities;

namespace PcAsCloud.DAL.Configurations;

public class ChannelUserConfiguration : IEntityTypeConfiguration<ChannelUser>
{
    public void Configure(EntityTypeBuilder<ChannelUser> builder)
    {
        builder.ToTable("ChannelUsers");

        builder.HasKey(cu => new { cu.ChannelId, cu.UserId });

        builder.Property(cu => cu.ChannelId)
            .IsRequired();

        builder.Property(cu => cu.UserId)
            .IsRequired()
            .HasMaxLength(450);

        builder.Property(cu => cu.JoinedDate)
            .IsRequired();

        builder.HasIndex(cu => cu.UserId);
        builder.HasIndex(cu => cu.JoinedDate);

        builder.HasOne(cu => cu.Channel)
            .WithMany(c => c.ChannelUsers)
            .HasForeignKey(cu => cu.ChannelId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cu => cu.User)
            .WithMany(u => u.ChannelUsers)
            .HasForeignKey(cu => cu.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
