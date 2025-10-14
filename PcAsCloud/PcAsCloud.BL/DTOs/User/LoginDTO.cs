namespace PcAsCloud.BL.DTOs.User;

public record LoginDTO
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
