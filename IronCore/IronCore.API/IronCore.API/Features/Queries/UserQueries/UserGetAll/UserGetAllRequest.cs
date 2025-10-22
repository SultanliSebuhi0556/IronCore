using MediatR;

namespace IronCore.API.Features.Queries.UserQueries.UserGetAll;
public class UserGetAllRequest : IRequest<IEnumerable<UserGetAllResponse>> { }