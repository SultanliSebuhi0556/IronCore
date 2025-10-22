using AutoMapper;
using IronCore.BL.Services.Instances;
using MediatR;

namespace IronCore.API.Features.Queries.MessageQueries.MessageGetChannelMessages;
public class MessageGetChannelMessagesHandler(IMessageService _messageService, IMapper _mapper) : IRequestHandler<MessageGetChannelMessagesRequest, IEnumerable<MessageGetChannelMessagesResponse>>
{
    public async Task<IEnumerable<MessageGetChannelMessagesResponse>> Handle(MessageGetChannelMessagesRequest request, CancellationToken cancellationToken)
    {
        var result = await _messageService.GetAllMessagesByChannelIdAsync(request.Id, cancellationToken);
        return _mapper.Map<IEnumerable<MessageGetChannelMessagesResponse>>(result);
    }
}