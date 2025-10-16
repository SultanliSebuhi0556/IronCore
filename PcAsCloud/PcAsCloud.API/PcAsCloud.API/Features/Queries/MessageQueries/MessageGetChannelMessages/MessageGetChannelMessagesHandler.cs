using MediatR;

namespace PcAsCloud.API.Features.Queries.MessageQueries.MessageGetChannelMessages;
public class MessageGetChannelMessagesHandler : IRequestHandler<MessageGetChannelMessagesRequest, IEnumerable<MessageGetChannelMessagesResponse>>
{
    Task<IEnumerable<MessageGetChannelMessagesResponse>> IRequestHandler<MessageGetChannelMessagesRequest, IEnumerable<MessageGetChannelMessagesResponse>>.Handle(MessageGetChannelMessagesRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}