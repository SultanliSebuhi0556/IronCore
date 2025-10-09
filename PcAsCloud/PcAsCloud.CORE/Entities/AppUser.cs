using Microsoft.AspNetCore.Identity;

namespace PcAsCloud.CORE.Entities;
public class AppUser : IdentityUser
{
    public ICollection<ChannelUser> ChannelUsers { get; set; }
}