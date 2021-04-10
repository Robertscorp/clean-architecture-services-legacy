using CleanArchitecture.Example.Domain.Enumerations;

namespace CleanArchitecture.Example.Domain.Entities
{

    public class User
    {

        #region - - - - - - Properties - - - - - -

        public string HashedPassword { get; set; }

        public UserRoleEnumeration Role { get; set; }

        public string UserName { get; set; }

        #endregion Properties

    }

}
