using MediatR;

namespace IronCore.API.Features.Queries.UserQueries.UserGetById;
public class UserGetByIdRequest : IRequest<UserGetByIdResponse>
{
    public string Id { get; set; }
}