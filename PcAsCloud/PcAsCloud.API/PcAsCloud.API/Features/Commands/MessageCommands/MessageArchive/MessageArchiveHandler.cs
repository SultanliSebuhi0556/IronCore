using MediatR;

namespace PcAsCloud.API.Features.Commands.MessageCommands.MessageArchive;
public class MessageArchiveHandler : IRequestHandler<MessageArchiveRequest, MessageArchiveResponse>
{
    public Task<MessageArchiveResponse> Handle(MessageArchiveRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}