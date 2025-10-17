using MediatR;

namespace PcAsCloud.API.Features.Queries.UserQueries.UserGetAllInChannel;
public class UserGetAllInChannelRequest : IRequest<IEnumerable<UserGetAllInChannelResponse>>
{
    public string ChannelId { get; set; }
}