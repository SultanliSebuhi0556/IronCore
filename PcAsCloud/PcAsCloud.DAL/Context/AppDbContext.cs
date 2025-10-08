using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PcAsCloud.CORE.Entities;

namespace PcAsCloud.DAL.Context;

public class AppDbContext : IdentityUserContext<AppUser>
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Channel> Channels { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}