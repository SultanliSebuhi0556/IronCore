using AutoMapper;
using MediatR;
using PcAsCloud.BL.DTOs.Channel;
using PcAsCloud.BL.Services.Instances;

namespace PcAsCloud.API.Features.Commands.ChannelCommands.ChannelCreate;
public class ChannelCreateHandler(IChannelServices _channelServices, IMapper _mapper) : IRequestHandler<ChannelCreateRequest, ChannelCreateResponse>
{
    public async Task<ChannelCreateResponse> Handle(ChannelCreateRequest request, CancellationToken cancellationToken)
    {
        var result = await _channelServices.CreateChannelAsync(_mapper.Map<ChannelCreateDTO>(request), cancellationToken);
        return new() { Id = result };
    }
}