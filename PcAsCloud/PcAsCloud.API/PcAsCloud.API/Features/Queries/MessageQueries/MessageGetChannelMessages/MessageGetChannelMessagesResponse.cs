namespace PcAsCloud.API.Features.Queries.MessageQueries.MessageGetChannelMessages;
public class MessageGetChannelMessagesResponse
{
    public string Id { get; set; }
    public string? Content { get; set; }
    public string? FileUrl { get; set; }
    public bool HaveRead { get; set; }
    public string SendedByUserId { get; set; }
}