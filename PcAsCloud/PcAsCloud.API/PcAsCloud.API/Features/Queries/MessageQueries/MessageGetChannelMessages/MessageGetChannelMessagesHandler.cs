using AutoMapper;
using MediatR;
using PcAsCloud.BL.Services.Instances;

namespace PcAsCloud.API.Features.Queries.MessageQueries.MessageGetChannelMessages;
public class MessageGetChannelMessagesHandler(IMessageService _messageService, IMapper _mapper) : IRequestHandler<MessageGetChannelMessagesRequest, IEnumerable<MessageGetChannelMessagesResponse>>
{
    public async Task<IEnumerable<MessageGetChannelMessagesResponse>> Handle(MessageGetChannelMessagesRequest request, CancellationToken cancellationToken)
    {
        var result = await _messageService.GetAllMessagesByChannelIdAsync(request.Id);
        return _mapper.Map<IEnumerable<MessageGetChannelMessagesResponse>>(result);
    }
}