namespace PcAsCloud.CORE.Entities;
public class Message : BaseEntity
{
    public string? Content { get; set; }
    public string? FileUrl { get; set; }
    //public bool HaveReaded { get; set; }
    public AppUser SendedBy { get; set; }
}