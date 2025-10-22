using MediatR;

namespace IronCore.API.Features.Commands.MessageCommands.MessageArchive;
public class MessageArchiveRequest : IRequest<MessageArchiveResponse>
{
    public string Id { get; set; }
}