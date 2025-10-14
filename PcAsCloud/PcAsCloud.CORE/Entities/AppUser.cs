using Microsoft.AspNetCore.Identity;

namespace PcAsCloud.CORE.Entities;
public class AppUser : IdentityUser
{
    public string ProfileImageUrl { get; set; }
    public ICollection<ChannelUser> ChannelUsers { get; set; }
}