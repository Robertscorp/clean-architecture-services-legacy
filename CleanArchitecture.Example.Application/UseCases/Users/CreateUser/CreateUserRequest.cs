using CleanArchitecture.Example.Application.Dtos;
using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Pipeline;

namespace CleanArchitecture.Example.Application.UseCases.Users.CreateUser
{

    public class CreateUserRequest : IUseCaseRequest<UserDto>
    {

        #region - - - - - - Properties - - - - - -

        public EntityID EmployeeRoleID { get; set; }

        public string FirstName { get; set; }

        public EntityID GenderID { get; set; }

        public string LastName { get; set; }

        public string PlainTextPassword { get; set; }

        public string UserName { get; set; }

        #endregion Properties

    }

}
