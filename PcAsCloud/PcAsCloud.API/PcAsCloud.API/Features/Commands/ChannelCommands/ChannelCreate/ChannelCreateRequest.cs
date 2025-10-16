using MediatR;

namespace PcAsCloud.API.Features.Commands.ChannelCommands.ChannelCreate;
public class ChannelCreateRequest : IRequest<ChannelCreateResponse>
{
    public bool IsDirect { get; set; }
    public string? ChannelName { get; set; }
    public string? TargetUserId { get; set; }
}