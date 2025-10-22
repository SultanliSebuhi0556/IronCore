using Microsoft.AspNetCore.Identity;

namespace IronCore.CORE.Entities;
public class AppUser : IdentityUser
{
    public string ProfileImageUrl { get; set; }
    public ICollection<ChannelUser> ChannelUsers { get; set; }
}