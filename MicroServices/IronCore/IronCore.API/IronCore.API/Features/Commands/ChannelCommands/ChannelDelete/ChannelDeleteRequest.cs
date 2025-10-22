using MediatR;

namespace IronCore.API.Features.Commands.ChannelCommands.ChannelDelete;
public class ChannelDeleteRequest : IRequest<ChannelDeleteResponse>
{
    public string Id { get; set; }
}