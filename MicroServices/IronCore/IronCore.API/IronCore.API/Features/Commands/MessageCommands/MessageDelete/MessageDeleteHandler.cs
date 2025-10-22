using IronCore.BL.Services.Instances;
using MediatR;

namespace IronCore.API.Features.Commands.MessageCommands.MessageDelete;
public class MessageDeleteHandler(IMessageService _messageService) : IRequestHandler<MessageDeleteRequest, MessageDeleteResponse>
{
    public async Task<MessageDeleteResponse> Handle(MessageDeleteRequest request, CancellationToken cancellationToken)
    {
        await _messageService.DeleteMessageAsync(request.Id, cancellationToken);
        return new();
    }
}