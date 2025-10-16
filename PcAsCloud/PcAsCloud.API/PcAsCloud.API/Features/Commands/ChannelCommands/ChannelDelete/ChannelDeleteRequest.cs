using MediatR;

namespace PcAsCloud.API.Features.Commands.ChannelCommands.ChannelDelete;
public class ChannelDeleteRequest : IRequest<ChannelDeleteResponse>
{
    public string Id { get; set; }
}