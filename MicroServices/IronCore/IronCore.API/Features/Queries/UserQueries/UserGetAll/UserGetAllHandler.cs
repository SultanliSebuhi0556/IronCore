using AutoMapper;
using IronCore.BL.Services.Instances;
using MediatR;

namespace IronCore.API.Features.Queries.UserQueries.UserGetAll;
public class UserGetAllHandler(IUserService _userService, IMapper _mapper) : IRequestHandler<UserGetAllRequest, IEnumerable<UserGetAllResponse>>
{
    public async Task<IEnumerable<UserGetAllResponse>> Handle(UserGetAllRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.GetAllUsersAsync(cancellationToken);
        return _mapper.Map<IEnumerable<UserGetAllResponse>>(result);
    }
}