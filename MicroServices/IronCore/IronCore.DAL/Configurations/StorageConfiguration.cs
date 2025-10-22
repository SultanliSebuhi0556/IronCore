using IronCore.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IronCore.DAL.Configurations;
public class StorageConfiguration : IEntityTypeConfiguration<Storage>
{
    public void Configure(EntityTypeBuilder<Storage> builder)
    {
        builder.ToTable("Storages");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedOnAdd();

        builder.Property(s => s.FileName)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(s => s.CreatedDate)
            .IsRequired();

        builder.Property(s => s.AppUserId)
            .IsRequired()
            .HasMaxLength(450);

        builder.HasIndex(s => s.AppUserId);
        builder.HasIndex(s => s.CreatedDate);

        builder.HasOne(s => s.AppUser)
            .WithMany()
            .HasForeignKey(s => s.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}