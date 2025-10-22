using MediatR;

namespace IronCore.API.Features.Queries.MessageQueries.MessageGetChannelMessages;
public class MessageGetChannelMessagesRequest : IRequest<IEnumerable<MessageGetChannelMessagesResponse>>
{
    public string Id { get; set; }
}