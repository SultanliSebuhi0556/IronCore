using MediatR;

namespace PcAsCloud.API.Features.Commands.ChannelCommands.ChannelJoin;
public class ChannelJoinRequest : IRequest<ChannelJoinResponse>
{
    public string Id { get; set; }
}