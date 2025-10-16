using MediatR;

namespace PcAsCloud.API.Features.Queries.UserQueries.UserGetById;
public class UserGetByIdHandler : IRequestHandler<UserGetByIdRequest, UserGetByIdResponse>
{
    public Task<UserGetByIdResponse> Handle(UserGetByIdRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}