using MediatR;

namespace PcAsCloud.API.Features.Queries.UserQueries.UserGetAll;
public class UserGetAllRequest : IRequest<IEnumerable<UserGetAllResponse>> { }