using MediatR;

namespace IronCore.API.Features.Queries.ChannelQueries.ChannelGetAll;
public class ChannelGetAllRequest : IRequest<IEnumerable<ChannelGetAllResponse>> { }