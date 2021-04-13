using CleanArchitecture.Services.Entities;

namespace CleanArchitecture.Example.Application.UseCases.Users.CreateUser
{

    public class UserDto
    {

        #region - - - - - - Properties - - - - - -

        public EntityID EmployeeID { get; set; }

        public EntityID EmployeeRoleID { get; set; }

        public string FirstName { get; set; }

        public EntityID GenderID { get; set; }

        public string HashedPassword { get; set; }

        public string LastName { get; set; }

        public EntityID PersonID { get; set; }

        public EntityID UserID { get; set; }

        public string UserName { get; set; }

        #endregion Properties

    }

}
