using Microsoft.AspNetCore.Http;

namespace PcAsCloud.BL.DTOs.User;
public record ChangeProfileImageDTO
{
    public string Id { get; set; }
    public IFormFile Image { get; set; }
}