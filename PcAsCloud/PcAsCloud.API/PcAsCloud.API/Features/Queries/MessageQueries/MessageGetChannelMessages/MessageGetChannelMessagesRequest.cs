using MediatR;

namespace PcAsCloud.API.Features.Queries.MessageQueries.MessageGetChannelMessages;
public class MessageGetChannelMessagesRequest : IRequest<IEnumerable<MessageGetChannelMessagesResponse>>
{
    public string Id { get; set; }
}