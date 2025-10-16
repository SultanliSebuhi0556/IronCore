using MediatR;

namespace PcAsCloud.API.Features.Queries.UserQueries.UserGetAll;
public class UserGetAllHandler : IRequestHandler<UserGetAllRequest, IEnumerable<UserGetAllResponse>>
{
    public Task<IEnumerable<UserGetAllResponse>> Handle(UserGetAllRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}