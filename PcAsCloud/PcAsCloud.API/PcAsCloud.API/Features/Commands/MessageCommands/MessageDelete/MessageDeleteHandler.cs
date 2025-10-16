using MediatR;

namespace PcAsCloud.API.Features.Commands.MessageCommands.MessageDelete;
public class MessageDeleteHandler : IRequestHandler<MessageDeleteRequest, MessageDeleteResponse>
{
    public Task<MessageDeleteResponse> Handle(MessageDeleteRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}