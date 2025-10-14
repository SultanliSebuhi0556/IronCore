namespace PcAsCloud.BL.DTOs.User;

public record UserGetDTO
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string ProfileImageUrl { get; set; }
}
