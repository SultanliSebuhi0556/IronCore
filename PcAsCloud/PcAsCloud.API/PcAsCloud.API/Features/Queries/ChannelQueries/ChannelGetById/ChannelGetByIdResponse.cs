namespace PcAsCloud.API.Features.Queries.ChannelQueries.ChannelGetById;
public class ChannelGetByIdResponse
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public bool IsDirect { get; set; }
    public IEnumerable<string> UserIds { get; set; }
}