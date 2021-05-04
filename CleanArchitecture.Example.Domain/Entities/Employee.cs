using CleanArchitecture.Services.Entities;

namespace CleanArchitecture.Example.Domain.Entities
{

    public class Employee : IEntity
    {

        #region - - - - - - Properties - - - - - -

        public Person EmployeeDetails { get; set; }

        public EmployeeRole Role { get; set; }

        public string Title { get; set; }

        public User User { get; set; }

        #endregion Properties

        #region - - - - - - IEntity Implementation - - - - - -

        public EntityID ID { get; set; }

        #endregion IEntity Implementation

    }

}
