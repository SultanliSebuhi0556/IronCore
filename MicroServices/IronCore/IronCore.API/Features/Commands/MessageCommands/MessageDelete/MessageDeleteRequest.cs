using MediatR;

namespace IronCore.API.Features.Commands.MessageCommands.MessageDelete;
public class MessageDeleteRequest : IRequest<MessageDeleteResponse>
{
    public string Id { get; set; }
}