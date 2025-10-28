using MediatR;

namespace IronCore.API.Features.Queries.MessageQueries.MessageGetAllBySearch;
public class MessageGetAllBySearchRequest : IRequest<IEnumerable<MessageGetAllBySearchResult>>
{
    public string ChannelId { get; set; }
    public string SearchText { get; set; }
}