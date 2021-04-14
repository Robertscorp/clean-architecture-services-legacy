using CleanArchitecture.Example.Application.Dtos;
using CleanArchitecture.Services.Pipeline;
using System.Linq;

namespace CleanArchitecture.Example.Application.UseCases.Users.GetUsers
{

    public class GetUsersRequest : IUseCaseRequest<IQueryable<UserDto>>
    {
    }

}
