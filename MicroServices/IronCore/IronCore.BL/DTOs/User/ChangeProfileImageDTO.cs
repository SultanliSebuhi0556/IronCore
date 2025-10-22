using Microsoft.AspNetCore.Http;

namespace IronCore.BL.DTOs.User;
public record ChangeProfileImageDTO
{
    public string Id { get; set; }
    public IFormFile Image { get; set; }
}