using CleanArchitecture.Example.Domain.Enumerations;

namespace CleanArchitecture.Example.Domain.Entities
{

    public class Employee
    {

        #region - - - - - - Properties - - - - - -

        public Person EmployeeDetails { get; set; }

        public EmployeeRoleEnumeration Role { get; set; }

        public string Title { get; set; }

        public User User { get; set; }

        #endregion Properties

    }

}
