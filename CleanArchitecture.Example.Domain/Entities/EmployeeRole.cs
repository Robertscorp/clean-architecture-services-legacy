using CleanArchitecture.Services.Entities;

namespace CleanArchitecture.Example.Domain.Entities
{

    public class EmployeeRole : StaticEntity
    {

        #region - - - - - - Fields - - - - - -

        public static readonly EmployeeRole Admin = new EmployeeRole("Administrator", 1);
        public static readonly EmployeeRole Mechanic = new EmployeeRole("Mechanic", 2);

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        private EmployeeRole(string name, int value) : base(name, value) { }

        #endregion Constructors

    }

}
