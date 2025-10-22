namespace IronCore.BL.DTOs.User;

public record LoginResponseDTO
{
    public string Id { get; set; }
    public string Token { get; set; }
}
