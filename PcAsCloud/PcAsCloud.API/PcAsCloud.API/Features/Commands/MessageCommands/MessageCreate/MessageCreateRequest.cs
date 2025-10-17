using MediatR;

namespace PcAsCloud.API.Features.Commands.MessageCommands.MessageCreate;
public class MessageCreateRequest : IRequest<MessageCreateResponse>
{
    public string ChannelId { get; set; }
    public string? Content { get; set; }
    public IFormFile? File { get; set; }
}