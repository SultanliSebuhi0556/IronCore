using IronCore.BL.Services.Instances;
using MediatR;

namespace IronCore.API.Features.Commands.MessageCommands.MessageArchive;
public class MessageArchiveHandler(IMessageService _messageService) : IRequestHandler<MessageArchiveRequest, MessageArchiveResponse>
{
    public async Task<MessageArchiveResponse> Handle(MessageArchiveRequest request, CancellationToken cancellationToken)
    {
        bool isArchived = await _messageService.ArchiveUnarchiveMessageAsync(request.Id, cancellationToken);
        return new MessageArchiveResponse() { IsArchived = isArchived };
    }
}