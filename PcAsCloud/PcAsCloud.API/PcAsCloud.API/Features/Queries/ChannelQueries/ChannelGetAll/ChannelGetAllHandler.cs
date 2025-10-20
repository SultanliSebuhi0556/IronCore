using AutoMapper;
using MediatR;
using PcAsCloud.BL.Services.Instances;

namespace PcAsCloud.API.Features.Queries.ChannelQueries.ChannelGetAll;
public class ChannelGetAllHandler(IChannelServices _channelServices, IMapper _mapper) : IRequestHandler<ChannelGetAllRequest, IEnumerable<ChannelGetAllResponse>>
{
    public async Task<IEnumerable<ChannelGetAllResponse>> Handle(ChannelGetAllRequest request, CancellationToken cancellationToken)
    {
        var result = await _channelServices.GetAllChannelsAsync();
        return _mapper.Map<IEnumerable<ChannelGetAllResponse>>(result);
    }
}