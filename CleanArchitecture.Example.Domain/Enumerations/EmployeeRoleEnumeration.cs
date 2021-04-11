using CleanArchitecture.Services.Enumerations;

namespace CleanArchitecture.Example.Domain.Enumerations
{

    public class EmployeeRoleEnumeration : Enumeration
    {

        #region - - - - - - Fields - - - - - -

        public static readonly EmployeeRoleEnumeration Admin = new EmployeeRoleEnumeration("Administrator", 1);
        public static readonly EmployeeRoleEnumeration Mechanic = new EmployeeRoleEnumeration("Mechanic", 2);

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        private EmployeeRoleEnumeration(string name, int value) : base(name, value) { }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public static implicit operator EmployeeRoleEnumeration(int value)
            => Get<EmployeeRoleEnumeration>(value);

        #endregion Methods

    }

}
