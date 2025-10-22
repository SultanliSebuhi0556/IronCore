namespace IronCore.API.Features.Queries.MessageQueries.MessageGetById;
public class MessageGetByIdResponse
{
    public string Id { get; set; }
    public string? Content { get; set; }
    public string? StorageId { get; set; }
    public bool IsRead { get; set; }
    public string SendedByUserId { get; set; }
}