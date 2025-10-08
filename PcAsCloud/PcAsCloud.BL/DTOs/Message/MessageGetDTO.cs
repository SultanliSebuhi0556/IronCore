namespace PcAsCloud.BL.DTOs.Message;
public record MessageGetDTO
{
    public string Id { get; set; }
    public string? Content { get; set; }
    public string? FileUrl { get; set; }
    public bool HaveReaded { get; set; }
    public string SendedByUserId { get; set; }
}
