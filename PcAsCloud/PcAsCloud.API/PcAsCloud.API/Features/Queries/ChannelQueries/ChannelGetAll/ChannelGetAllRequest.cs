using MediatR;

namespace PcAsCloud.API.Features.Queries.ChannelQueries.ChannelGetAll;
public class ChannelGetAllRequest : IRequest<IEnumerable<ChannelGetAllResponse>> { }