using MediatR;

namespace PcAsCloud.API.Features.Queries.ChannelQueries.ChannelGetAll;
public class ChannelGetAllHandler : IRequestHandler<ChannelGetAllRequest, IEnumerable<ChannelGetAllResponse>>
{
    public Task<IEnumerable<ChannelGetAllResponse>> Handle(ChannelGetAllRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}