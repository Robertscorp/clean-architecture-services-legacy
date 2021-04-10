using CleanArchitecture.Services.Enumerations;

namespace CleanArchitecture.Example.Domain.Enumerations
{

    public class UserRoleEnumeration : Enumeration
    {

        #region - - - - - - Fields - - - - - -

        public static readonly UserRoleEnumeration Admin = new UserRoleEnumeration("Administrator", 1);
        public static readonly UserRoleEnumeration Mechanic = new UserRoleEnumeration("Mechanic", 2);

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        private UserRoleEnumeration(string name, int value) : base(name, value) { }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public static implicit operator UserRoleEnumeration(int value)
            => Get<UserRoleEnumeration>(value);

        #endregion Methods

    }

}
