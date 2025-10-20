using MediatR;
using PcAsCloud.BL.Services.Instances;

namespace PcAsCloud.API.Features.Commands.MessageCommands.MessageArchive;
public class MessageArchiveHandler(IMessageService _messageService) : IRequestHandler<MessageArchiveRequest, MessageArchiveResponse>
{
    public async Task<MessageArchiveResponse> Handle(MessageArchiveRequest request, CancellationToken cancellationToken)
    {
        bool isArchived = await _messageService.ArchiveUnarchiveMessageAsync(request.Id);
        return new MessageArchiveResponse() { IsArchived = isArchived };
    }
}