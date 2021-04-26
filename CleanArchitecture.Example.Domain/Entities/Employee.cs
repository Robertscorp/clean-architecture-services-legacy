using CleanArchitecture.Example.Domain.Enumerations;
using CleanArchitecture.Services.Entities;

namespace CleanArchitecture.Example.Domain.Entities
{

    public class Employee
    {

        #region - - - - - - Properties - - - - - -

        public EntityID ID { get; set; }

        public Person EmployeeDetails { get; set; }

        public EmployeeRoleEnumeration Role { get; set; }

        public string Title { get; set; }

        public User User { get; set; }

        #endregion Properties

    }

}
