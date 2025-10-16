using MediatR;

namespace PcAsCloud.API.Features.Commands.MessageCommands.MessageCreate;
public class MessageCreateHandler : IRequestHandler<MessageCreateRequest, MessageCreateResponse>
{
    public Task<MessageCreateResponse> Handle(MessageCreateRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}